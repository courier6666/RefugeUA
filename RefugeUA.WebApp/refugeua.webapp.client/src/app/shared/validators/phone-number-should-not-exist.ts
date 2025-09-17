import { ValidatorFn, ValidationErrors, AbstractControl, AsyncValidatorFn } from "@angular/forms";
import { Injector } from "@angular/core";
import { AuthService } from "../../core/services/auth/auth-service";
import { firstValueFrom } from "rxjs";

export const phoneNumberShouldNotExist = (authService: AuthService): AsyncValidatorFn => {
    return async (control: AbstractControl): Promise<ValidationErrors | null> => {
        console.log(control);
        const phoneNumber = control.value;
        if(await firstValueFrom(authService.phoneNumberExists(phoneNumber)))
        {
            return {'phoneNumberShouldNotExist': 'phone number already exists'} as ValidationErrors;
        }

        return null;
    };
}