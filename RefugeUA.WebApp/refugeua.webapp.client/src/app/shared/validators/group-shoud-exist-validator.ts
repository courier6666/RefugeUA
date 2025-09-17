import { ValidatorFn, ValidationErrors, AbstractControl, AsyncValidatorFn } from "@angular/forms";
import { Injector } from "@angular/core";
import { AuthService } from "../../core/services/auth/auth-service";
import { firstValueFrom } from "rxjs";
import { AnnouncementService } from "../../core/services/announcements/announcement-service";

export const groupShouldExist = (announcementService: AnnouncementService): AsyncValidatorFn => {
    return async (control: AbstractControl): Promise<ValidationErrors | null> => {

        const name = control.value;
        if (await firstValueFrom(announcementService.groupByNameExists(name))) {
            return null;
        }
        
        return { 'groupMustExist': 'group does not exist!' } as ValidationErrors;
    };
}