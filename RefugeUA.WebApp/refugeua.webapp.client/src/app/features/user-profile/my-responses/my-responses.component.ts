import { Component } from '@angular/core';
import { Announcement, AnnouncementResponse } from '../../../core/models';
import { ActivatedRoute } from '@angular/router';
import { AnnouncementService } from '../../../core/services/announcements/announcement-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-my-responses',
  standalone: false,
  templateUrl: './my-responses.component.html',
  styleUrl: './my-responses.component.css'
})
export class MyResponsesComponent {
  responses: AnnouncementResponse[] = [];
  announcementId!: number;

  currentPage: number = 1;
  responsesLoaded = false;
  responsesFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  subscription!: Subscription;

  constructor(private announceService: AnnouncementService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;
    this.announcementId = id;

    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      if (params['page']) {
        this.currentPage = +params['page'];
      }
      else {
        this.currentPage = 1;
      }

      this.loadResponses();
    });
  }

  loadResponses() {

    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.responsesLoaded = false;
    this.responsesFound = undefined;
    this.subscription = this.announceService.getMyResponses(this.currentPage, 10, this.prompt).subscribe({
      next: (response) => {
        this.responses = response.items;
        this.pages = response.pagesCount;
        this.responsesFound = response.totalCount > 0;
        this.responsesLoaded = true;
      },
      error: (error) => {
        this.responsesLoaded = true;
        this.responsesFound = false;
      }
    });
  }

  onPageNavigation(page: number) {
    window.scrollTo(0, 0);
    this.currentPage = page;
    this.loadResponses();
  }

  getNavigationCallback(): (page: number) => void {
    return (page: number) => this.onPageNavigation(page);
  }

  getOnSearchBarCallback(): (prompt: string | null) => void {
    return (prompt: string | null) => {
      this.prompt = prompt;
      this.currentPage = 1;
      this.loadResponses();
    }
  }

  getRouterLinkAnnouncement(announcement: Announcement): string {
    switch (announcement.type) {
      case 'Work':
        return `/announcements/work/${announcement.id}/detail`;
      case 'Education':
        return `/announcements/education/${announcement.id}/detail`;
      case 'Accomodation':
        return `/announcements/accomodation/${announcement.id}/detail`;
    }

    return '';
  }
}
