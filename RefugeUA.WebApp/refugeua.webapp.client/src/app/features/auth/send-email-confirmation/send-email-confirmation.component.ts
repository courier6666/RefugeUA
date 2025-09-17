import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../../core/services/auth/auth-service';
import { Location } from '@angular/common';
import { SendEmailConfirmation } from '../../../core/api-models/send-email-confirmation';

@Component({
  selector: 'app-send-email-confirmation',
  standalone: false,
  templateUrl: './send-email-confirmation.component.html',
  styleUrl: './send-email-confirmation.component.css'
})
export class SendEmailConfirmationComponent implements OnInit {

  sendEmailConfirmationForm: FormGroup = new FormGroup({});
  isSubmitted = false;
  resultSuccess: boolean | null = null;

  constructor(private authService: AuthService, private location: Location) {

  }

  ngOnInit(): void {
    this.sendEmailConfirmationForm = new FormGroup({
      email: new FormControl<string>(''),
      password: new FormControl<string>(''),
    });
    
    this.sendEmailConfirmationForm.valueChanges.subscribe({
      next: (result) => this.isSubmitted = false
    });
  }

  onSubmit() {

    this.isSubmitted = true;
    this.resultSuccess = null;

    let sendEmailConfirmation: SendEmailConfirmation = {
      email: this.sendEmailConfirmationForm.value.email,
      password: this.sendEmailConfirmationForm.value.password
    }
    
    this.authService.sendEmailConfirmation(sendEmailConfirmation).subscribe({
      next: (result) => this.resultSuccess = true,
      error: (err) => this.resultSuccess = false
    });
  }

  onCancel() {
    this.location.back();
  }
}
