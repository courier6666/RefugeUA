import { Component, Input } from '@angular/core';
import { AuthService } from '../../../../../core/services/auth/auth-service';
import { AnnouncementModerationService } from '../../../../../core/services/announcements/announcements-moderation-service';
import { Announcement } from '../../../../../core/models';
import { Roles } from '../../../../../core/constants/user-roles-constants';

@Component({
  selector: 'app-announcement-detail-acceptence',
  standalone: false,
  templateUrl: './announcement-detail-acceptence.component.html',
  styleUrl: './announcement-detail-acceptence.component.css'
})
export class AnnouncementDetailAcceptenceComponent {

  Roles = Roles;

  @Input({required: true}) announcement!: Announcement;
  @Input({required: true}) isAuthenticated!: boolean;
  @Input({required: true}) isAllowedToEdit!: boolean;

  acceptAnnouncementSuccess?: boolean;
  isAcceptLoading: boolean = false;

  constructor(public authService: AuthService, private announcementModerationService: AnnouncementModerationService) {
    
  }

  onRejectAnnouncement(): void {
    this.isAcceptLoading = true;
    this.announcementModerationService.rejectAnnouncement(this.announcement.id!).subscribe({
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
    this.isAcceptLoading = true;
    this.announcementModerationService.acceptAnnouncement(this.announcement.id!).subscribe({
      next: () => {
        this.announcement.isAccepted = true;
        this.isAcceptLoading = false;
        this.announcement.nonAcceptenceReason = undefined;
      },
      error: (error) => {
        console.error("Error accepting announcement:", error);
        this.isAcceptLoading = false;
      }
    });
  }
}
