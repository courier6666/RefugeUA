import { Component, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { UserRole } from '../../../core/enums/user-role';
import { AuthService } from '../../../core/services/auth/auth-service';
import { ValidatorFn, ValidationErrors, AbstractControl } from '@angular/forms';
import { RegisterCommand } from '../../../core/api-models/register-command';
import { emailShouldNotExist } from '../../../shared/validators/email-should-not-exist-validator';
import { phoneNumberShouldNotExist } from '../../../shared/validators/phone-number-should-not-exist';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit {
  UserRole = UserRole;
  registerForm: FormGroup = new FormGroup({});
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  submitErrorResult: string[] = [];

  constructor(private authService: AuthService, private location: Location) {

  }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      email: new FormControl<string>('', {
        validators: [
          Validators.required,
          Validators.email,
          Validators.maxLength(200)
        ],
        asyncValidators: [emailShouldNotExist(this.authService)]
      }),
      firstName: new FormControl<string>('', [
        Validators.required,
        Validators.maxLength(100)
      ]),
      lastName: new FormControl<string>('', [
        Validators.required,
        Validators.maxLength(100)
      ]),
      role: new FormControl<UserRole | null>(null, [
        Validators.required
      ]),
      dateOfBirth: new FormControl<Date | null>(null, [
        Validators.required,
        this.dateInPastValidator()
      ]),
      phoneNumber: new FormControl<string>('', {
        validators: [
          Validators.required,
          Validators.pattern(/^\+?[1-9]\d{10,14}$/)
        ],
        asyncValidators: [phoneNumberShouldNotExist(this.authService)]
      }),
      district: new FormControl<string | null>('',),
      password: new FormControl<string>('', [
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(16),
        Validators.pattern(/[A-Z]/), // at least one uppercase
        Validators.pattern(/[a-z]/), // at least one lowercase
        Validators.pattern(/[0-9]/), // at least one number
        Validators.pattern(/[\!\?\*\._]/) // at least one special character
      ]),
      confirmPassword: new FormControl<string>('', [
        Validators.required
      ])
    }, {
      validators: this.passwordMatchValidator()
    });

    this.registerForm.valueChanges.subscribe({
      next: (result) => {
        this.isFormSubmitted = false;
        let formValues = this.registerForm.value;
        this.district?.clearValidators();
        if (this.role?.value == UserRole.CommunityAdmin) {
          this.district?.addValidators([Validators.required, Validators.maxLength(100)]);
        }
        else {
          this.district?.addValidators([Validators.maxLength(100)]);
        }

        this.district?.updateValueAndValidity({ emitEvent: false });
      }
    });
  }


  get maxDateForBirthday(): Date {
    let nowDate = new Date();
    nowDate.setFullYear(nowDate.getFullYear() - 16);
    return nowDate;
  }

  dateInPastValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const date = control.value;
      return date && new Date(date) >= new Date()
        ? { dateNotInPast: true }
        : null;
    };
  }

  passwordMatchValidator(): ValidatorFn {
    return (group: AbstractControl): ValidationErrors | null => {
      const password = group.get('password')?.value;
      const confirmPassword = group.get('confirmPassword')?.value;
      return password === confirmPassword ? null : { passwordsMismatch: true };
    };
  }

  get firstName() { return this.registerForm.get('firstName'); }
  get lastName() { return this.registerForm.get('lastName'); }
  get email() { return this.registerForm.get('email'); }
  get phoneNumber() { return this.registerForm.get('phoneNumber'); }
  get district() { return this.registerForm.get('district'); }
  get dateOfBirth() { return this.registerForm.get('dateOfBirth'); }
  get password() { return this.registerForm.get('password'); }
  get confirmPassword() { return this.registerForm.get('confirmPassword'); }
  get role() { return this.registerForm.get('role'); }

  onCancel() {
    this.location.back();
  }

  onSubmit() {
    this.resultSuccess = null;
    this.isFormSubmitted = true;
    let registerCommand: RegisterCommand = {
      firstName: this.firstName?.value,
      lastName: this.lastName?.value,
      role: UserRole[this.role?.value],
      dateOfBirth: this.dateOfBirth?.value,
      email: this.email?.value,
      phoneNumber: this.phoneNumber?.value,
      password: this.password?.value,
      confirmPassword: this.confirmPassword?.value,
      district: this.district?.value
    };

    console.log(registerCommand);

    this.authService.register(registerCommand).subscribe({
      next: (res) => {
        this.resultSuccess = true;
      },
      error: (err) => {
        this.resultSuccess = false;
        this.submitErrorResult = err.message.split('\n');
      }
    });
  }
}
