import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { AnnouncementService } from '../../../../core/services/announcements/announcement-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-response-create',
  standalone: false,
  templateUrl: './response-create.component.html',
  styleUrl: './response-create.component.css'
})
export class ResponseCreateComponent implements OnInit {
  @Input({ required: true }) announcementId: number = 0;

  isResponseCreatedForUser?: boolean = undefined;

  constructor(public authService: AuthService, public announceService: AnnouncementService, private router: Router) {

  }

  contactForm: FormGroup = new FormGroup({});

  ngOnInit(): void {
    this.isResponseCreatedForUser = undefined;
    this.announceService.myResponseExistsForAnnouncement(this.announcementId).subscribe({
      next: (res) => this.isResponseCreatedForUser = res,
      error: (err) => console.error(err)
    });

    this.contactForm = new FormGroup({
      phoneNumber: new FormControl<string>('', [Validators.required, Validators.pattern(/^\+?[1-9]\d{10,14}$/)]),
      email: new FormControl<string | null>(null, [Validators.email, Validators.maxLength(128)]),
      telegram: new FormControl<string | null>(null, Validators.maxLength(256)),
      viber: new FormControl<string | null>(null, Validators.maxLength(256)),
      facebook: new FormControl<string | null>(null, Validators.maxLength(256))
    });

    this.contactForm.get('phoneNumber')?.setValue(this.authService.userClaims.phoneNumber);
  }

  onResponseSubmit() {
    let valBefore = this.isResponseCreatedForUser;
    this.isResponseCreatedForUser = undefined;
    let formValue = this.contactForm.value;
    this.announceService.createResponse(this.announcementId, {
      phoneNumber: formValue.phoneNumber,
      email: formValue.email ?? undefined,
      telegram: formValue.telegram ?? undefined,
      viber: formValue.viber ?? undefined,
      facebook: formValue.facebook ?? undefined
    }).subscribe({
      next: (response) => {
        this.isResponseCreatedForUser = true;
      },
      error: (err) => {
        console.error(err);
        this.isResponseCreatedForUser = valBefore;
      }
    });
  }

  onResponseRemove() {
    let valBefore = this.isResponseCreatedForUser;
    this.isResponseCreatedForUser = undefined;

    this.announceService.removeMyResponse(this.announcementId).subscribe({
      next: (response) => {
        this.isResponseCreatedForUser = false;
      },
      error: (err) => {
        this.isResponseCreatedForUser = valBefore;
      }
    });
  }
}
