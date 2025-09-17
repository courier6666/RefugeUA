import { CommonModule, DatePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { Announcement } from '../../../../core/models';
import { AnnouncementService } from '../../../../core/services/announcements/announcement-service';
import { shortenContent } from '../../../../shared/util/content-shortening';

@Component({
  selector: 'app-my-announcement-preview',
  standalone: false,
  templateUrl: './my-announcement-preview.component.html',
  styleUrl: './my-announcement-preview.component.css'
})
export class MyAnnouncementPreviewComponent {
  @Input({ required: true }) announcement!: Announcement;

  acceptAnnouncementSuccess?: boolean;
  isAcceptLoading: boolean = false;
  closeStateAnnouncementSuccess?: boolean;
  isCloseStateLoading: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute, private announcementService: AnnouncementService) {

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
    this.isCloseStateLoading = true;
    this.announcementService.open(this.announcement.id!).subscribe({
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
    this.isCloseStateLoading = true;
    this.announcementService.close(this.announcement.id!).subscribe({
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
}
