import { Component } from '@angular/core';
import { AnnouncementService } from '../../../core/services/announcements/announcement-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Announcement } from '../../../core/models';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-my-announcements',
  standalone: false,
  templateUrl: './my-announcements.component.html',
  styleUrl: './my-announcements.component.css'
})
export class MyAnnouncementsComponent {
  announcements: Announcement[] = [];
  currentPage: number = 1;
  announcementsLoaded = false;
  announcementsFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  subscription!: Subscription;
  
  constructor(private announcementService: AnnouncementService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      if (params['page']) {
        this.currentPage = +params['page'];
      }
      else {
        this.currentPage = 1;
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
    this.subscription = this.announcementService.getMyAnnouncements(this.currentPage, 10, this.prompt).subscribe({
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
