import { Component, OnInit } from '@angular/core';
import { FormGroup, ReactiveFormsModule, FormControl, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { AuthService } from '../../../core/services/auth/auth-service';
import { emailShouldExist } from '../../../shared/validators/email-should-exist-validator';
import { SessionStorageService } from '../../../core/services/util/session-storage-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup({});
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  resultMessage: string = '';

  constructor(private authService: AuthService, private router: Router, private location: Location, private sessionStorageService: SessionStorageService) {

  }

  ngOnInit(): void {

    if(this.sessionStorageService.getItem('sessionExpired') === 'true')
    {
      this.isFormSubmitted = true;
      this.resultSuccess = false;
      this.resultMessage = "Оновіть дані сесії";
      this.sessionStorageService.setItem('sessionExpired', 'false');
    }

    this.loginForm = new FormGroup({
      email: new FormControl<string>('', {
        validators: [Validators.required],
        asyncValidators: [emailShouldExist(this.authService)]
      }),
      password: new FormControl<string>('', {
        validators: [Validators.required]
      }),
    });

    this.loginForm.valueChanges.subscribe({
      next: (result) => this.isFormSubmitted = false
    });
  }

  get email() { return this.loginForm.get('email'); }
  get password() { return this.loginForm.get('password'); }

  onSubmit() {
    this.resultSuccess = null;
    this.isFormSubmitted = true;

    this.authService.login(this.loginForm.value.email, this.loginForm.value.password).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/']);
      },
      error: (err) => {
        this.resultMessage = err.message;
        this.resultSuccess = false;
      }
    });
  }

  onCancel() {
    this.location.back();
  }
}
