import { Component, OnInit } from '@angular/core';
import { AnnouncementModerationService } from '../../../core/services/announcements/announcements-moderation-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-announcements-moderation',
  standalone: false,
  templateUrl: './announcements-moderation.component.html',
  styleUrl: './announcements-moderation.component.css'
})
export class AnnouncementsModerationComponent implements OnInit {
  announcements: any[] = [];
  currentPage: number = 1;
  announcementsLoaded = false;
  announcementsFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  subscription!: Subscription;

  constructor(private announcementService: AnnouncementModerationService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params['page']) {
        this.currentPage = +params['page'];
      }

      this.loadAnnouncements();
    });

  }

  getNavigationCallback(): (page: number) => void {
    return (page: number) => this.onPageNavigation(page);
  }

  getOnSearchBarCallback(): (prompt: string | null) => void {
    return (prompt: string | null) => {
      this.prompt = prompt;
      this.currentPage = 1;
      this.loadAnnouncements();
    }
  }

  onPageNavigation(page: number) {
    window.scrollTo(0, 0);
    this.currentPage = page;

    let queryParams = { page: page };
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: queryParams,
      queryParamsHandling: 'merge'
    });
  }

  loadAnnouncements() {
    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.announcementsLoaded = false;
    this.announcementsFound = undefined;
    this.subscription = this.announcementService.getAnnouncementsForModeration(this.currentPage, 10, this.prompt).subscribe({
      next: (response) => {
        this.announcements = response.items;
        this.pages = response.pagesCount;
        this.announcementsFound = response.totalCount > 0;
        this.announcementsLoaded = true;
      },
      error: (error) => {
        this.announcementsLoaded = true;
        this.announcementsFound = false;
      }
    });
  }

}
