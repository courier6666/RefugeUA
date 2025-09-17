import { ValidatorFn, ValidationErrors, AbstractControl, AsyncValidatorFn } from "@angular/forms";
import { Injector } from "@angular/core";
import { AuthService } from "../../core/services/auth/auth-service";
import { firstValueFrom } from "rxjs";

export const scheduleItemMustBetweenDates = (startDate: AbstractControl<any, any>, endDate: AbstractControl<any, any>): ValidatorFn => {
    return (control: AbstractControl): ValidationErrors | null => {

        var itemTime = new Date(control.value);

        var startDateVal = new Date(startDate.value);
        startDateVal.setHours(0 ,0, 0, 0);

        var endDateVal = new Date(endDate.value);
        endDateVal.setHours(23, 59, 59, 999);

        if (startDateVal != null && itemTime < startDateVal) {
            return { 'itemTimeMustBeAfterStartTime': 'Start time of schedule item is less than start time of event.' };
        }

        if (endDateVal != null && itemTime > endDateVal) {
            return { 'itemTimeMustBeBeforeEndTime': 'End time of schedule item is greater than end time of event.' };
        }

        return null;
    };
}