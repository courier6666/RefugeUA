import { Component, Input, OnInit } from '@angular/core';
import { Announcement } from '../../../core/models';
import { AnnouncementModerationService } from '../../../core/services/announcements/announcements-moderation-service';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-announcement-acceptence-reason-input',
  standalone: true,
  templateUrl: './announcement-acceptence-reason-input.component.html',
  styleUrl: './announcement-acceptence-reason-input.component.css',
  imports: [ReactiveFormsModule, CommonModule]
})
export class AnnouncementAcceptenceReasonInputComponent implements OnInit {
  @Input({ required: true }) announcement!: Announcement;

  acceptenceReasonFormGroup: FormGroup = new FormGroup([]);

  isAcceptenceLoading = false;
  acceptenceChangeSuccessStatus?: boolean;

  constructor(private announcementModerationService: AnnouncementModerationService) {

  }

  ngOnInit(): void {
    let formBuilder = new FormBuilder();
    this.acceptenceReasonFormGroup = formBuilder.group({
      acceptenceReason: new FormControl<string | null>('', Validators.maxLength(500))
    });

    this.acceptenceReason?.setValue(this.announcement.nonAcceptenceReason);
    this.acceptenceReasonFormGroup.valueChanges.subscribe((value) => {
      this.acceptenceChangeSuccessStatus = undefined;
    });
  }

  get acceptenceReason() {
    return this.acceptenceReasonFormGroup.get('acceptenceReason');
  }

  onCancel() {
    console.log(this.announcement.nonAcceptenceReason);
    this.acceptenceReason?.setValue(this.announcement.nonAcceptenceReason);
  }

  onSubmit() {
    this.isAcceptenceLoading = true;
    this.acceptenceChangeSuccessStatus = undefined;
    this.announcementModerationService.setNonAcceptenceReason(this.announcement.id!, this.acceptenceReason?.value ?? '').subscribe({
      next: () => {
        this.isAcceptenceLoading = false;
        this.acceptenceChangeSuccessStatus = true;
        this.announcement.nonAcceptenceReason = this.acceptenceReason?.value ?? '';
      },
      error: () => {
        this.acceptenceChangeSuccessStatus = false;
        this.isAcceptenceLoading = false;
      }
    });
  }

  onRejectAnnouncement(): void {
    this.isAcceptenceLoading = true;
    this.announcementModerationService.rejectAnnouncement(this.announcement.id!).subscribe({
      next: () => {
        this.announcement.isAccepted = false;
        this.isAcceptenceLoading = false;
      },
      error: (error) => {
        console.error("Error rejecting announcement:", error);
        this.isAcceptenceLoading = false;
      }
    });
  }

  onAcceptAnnouncement(): void {
    this.isAcceptenceLoading = true;
    this.announcementModerationService.acceptAnnouncement(this.announcement.id!).subscribe({
      next: () => {
        this.announcement.isAccepted = true;
        this.isAcceptenceLoading = false;
      },
      error: (error) => {
        console.error("Error accepting announcement:", error);
        this.isAcceptenceLoading = false;
      }
    });
  }
}
