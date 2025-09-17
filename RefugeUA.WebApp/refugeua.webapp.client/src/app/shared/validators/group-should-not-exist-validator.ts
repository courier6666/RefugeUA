import { ValidatorFn, ValidationErrors, AbstractControl, AsyncValidatorFn } from "@angular/forms";
import { Injector } from "@angular/core";
import { AuthService } from "../../core/services/auth/auth-service";
import { firstValueFrom } from "rxjs";
import { AnnouncementService } from "../../core/services/announcements/announcement-service";

export const groupShouldNotExist = (announcementService: AnnouncementService): AsyncValidatorFn => {
    return async (control: AbstractControl): Promise<ValidationErrors | null> => {

        const name = control.value;
        
        if(await firstValueFrom(announcementService.groupByNameExists(name)))
        {
            console.log(name);
            return {'groupMustNotExist': 'group already exists!'} as ValidationErrors;
        }

        return null;
    };
}