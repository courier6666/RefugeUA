import { ValidatorFn, ValidationErrors, AbstractControl, AsyncValidatorFn } from "@angular/forms";
import { Injector } from "@angular/core";
import { AuthService } from "../../core/services/auth/auth-service";
import { firstValueFrom } from "rxjs";

export const emailShouldExist = (authService: AuthService): AsyncValidatorFn => {
    return async (control: AbstractControl): Promise<ValidationErrors | null> => {

        const email = control.value;
        if (await firstValueFrom(authService.emailExists(email))) {
            return null;
        }
        
        return { 'emailShouldExist': 'email does not exist' } as ValidationErrors;
    };
}