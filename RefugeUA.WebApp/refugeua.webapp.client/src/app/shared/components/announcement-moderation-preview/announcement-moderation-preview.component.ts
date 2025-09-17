import { CommonModule, DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Announcement } from '../../../core/models';
import { AnnouncementModerationService } from '../../../core/services/announcements/announcements-moderation-service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { shortenContent } from '../../../shared/util/content-shortening';
import { AddressComponent } from '../address/address.component';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-announcement-moderation-preview',
  standalone: true,
  templateUrl: './announcement-moderation-preview.component.html',
  styleUrl: './announcement-moderation-preview.component.css',
  imports: [DatePipe, AddressComponent, CommonModule, RouterModule]
})
export class AnnouncementModerationPreviewComponent implements OnInit {
  @Input({ required: true }) announcement!: Announcement;

  acceptAnnouncementSuccess?: boolean;
  isAcceptLoading: boolean = false;
  closeStateAnnouncementSuccess?: boolean;
  isCloseStateLoading: boolean = false;

  openCloseSubscription!: Subscription;
  acceptRejectSubscription!: Subscription;

  constructor(private router: Router, private route: ActivatedRoute, private announcementModerationService: AnnouncementModerationService) {

  }

  ngOnInit(): void {

  }

  get shortContent(): string {
    return shortenContent(this.announcement.content, 200);
  }

  get routerLinkDetail(): string {
    let announcementType = this.announcement.type?.toLowerCase();

    let routerLink = `/announcements/${announcementType}/${this.announcement.id}/detail`;

    return routerLink;
  }
  onOpen(): void {

    if (this.openCloseSubscription) {
      this.openCloseSubscription.unsubscribe();
    }

    this.isCloseStateLoading = true;
    this.openCloseSubscription = this.announcementModerationService.open(this.announcement.id!).subscribe({
      next: () => {
        this.announcement.isClosed = false;
        this.isCloseStateLoading = false;
      },
      error: (error) => {
        console.error("Error opening announcement:", error);
        this.isCloseStateLoading = false;
      }
    });
  }

  onClosed(): void {
    if (this.openCloseSubscription) {
      this.openCloseSubscription.unsubscribe();
    }

    this.isCloseStateLoading = true;
    this.openCloseSubscription = this.announcementModerationService.close(this.announcement.id!).subscribe({
      next: () => {
        this.announcement.isClosed = true;
        this.isCloseStateLoading = false;
      },
      error: (error) => {
        console.error("Error closing announcement:", error);
        this.isCloseStateLoading = false;
      }
    });
  }

  onRejectAnnouncement(): void {

    if (this.acceptRejectSubscription) {
      this.acceptRejectSubscription.unsubscribe();
    }

    this.isAcceptLoading = true;
    this.acceptRejectSubscription = this.announcementModerationService.rejectAnnouncement(this.announcement.id!).subscribe({
      next: () => {
        this.announcement.isAccepted = false;
        this.isAcceptLoading = false;
      },
      error: (error) => {
        console.error("Error rejecting announcement:", error);
        this.isAcceptLoading = false;
      }
    });
  }

  onAcceptAnnouncement(): void {
    if (this.acceptRejectSubscription) {
      this.acceptRejectSubscription.unsubscribe();
    }

    this.isAcceptLoading = true;
    this.acceptRejectSubscription = this.announcementModerationService.acceptAnnouncement(this.announcement.id!).subscribe({
      next: () => {
        this.announcement.isAccepted = true;
        this.isAcceptLoading = false;
      },
      error: (error) => {
        console.error("Error accepting announcement:", error);
        this.isAcceptLoading = false;
      }
    });
  }
}
