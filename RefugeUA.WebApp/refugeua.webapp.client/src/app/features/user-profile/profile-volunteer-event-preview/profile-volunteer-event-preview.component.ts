import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { VolunteerEvent } from '../../../core/models';
import { shortenContent } from '../../../shared/util/content-shortening';
import { Input } from '@angular/core';
import { VolunteerEventService } from '../../../core/services/volunteer/volunteer-event-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-profile-volunteer-event-preview',
  standalone: false,
  templateUrl: './profile-volunteer-event-preview.component.html',
  styleUrl: './profile-volunteer-event-preview.component.css'
})
export class ProfileVolunteerEventPreviewComponent {
  @Input({ required: true }) volunteerEvent!: VolunteerEvent;
  @Input({ required: true }) fromPage!: string;

  closeStateAnnouncementSuccess?: boolean;
  isCloseStateLoading: boolean = false;

  openCloseSubscription!: Subscription;

  constructor(private router: Router, private route: ActivatedRoute, private volunteerEventService: VolunteerEventService) {

  }

  public get shortContent(): string {
    return shortenContent(this.volunteerEvent?.content!, 200);
  }

  onOpen(): void {

    if (this.openCloseSubscription) {
      this.openCloseSubscription.unsubscribe();
    }

    this.isCloseStateLoading = true;
    this.openCloseSubscription = this.volunteerEventService.openVolunteerEvent(this.volunteerEvent.id!).subscribe({
      next: () => {
        this.volunteerEvent.isClosed = false;
        this.isCloseStateLoading = false;
      },
      error: (error) => {
        console.error("Error closing announcement:", error);
        this.isCloseStateLoading = false;
      }
    });
  }

  onClosed(): void {

    if (this.openCloseSubscription) {
      this.openCloseSubscription.unsubscribe();
    }

    this.isCloseStateLoading = true;
    this.openCloseSubscription = this.volunteerEventService.closeVolunteerEvent(this.volunteerEvent.id!).subscribe({
      next: () => {
        this.volunteerEvent.isClosed = true;
        this.isCloseStateLoading = false;
      },
      error: (error) => {
        console.error("Error closing announcement:", error);
        this.isCloseStateLoading = false;
      }
    });
  }
}
