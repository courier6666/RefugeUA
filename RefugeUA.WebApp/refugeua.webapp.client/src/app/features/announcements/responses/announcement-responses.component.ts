import { Component, OnInit, Input } from '@angular/core';
import { Announcement, AnnouncementResponse } from '../../../core/models';
import { AnnouncementService } from '../../../core/services/announcements/announcement-service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-announcement-responses',
  standalone: false,
  templateUrl: './announcement-responses.component.html',
  styleUrl: './announcement-responses.component.css'
})
export class AnnouncementResponsesComponent implements OnInit {
  responses: AnnouncementResponse[] = [];
  announcement!: Announcement;
  announcementId!: number;

  currentPage: number = 1;
  responsesLoaded = false;
  responsesFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  announcementFoundSuccess?: boolean;

  constructor(private announceService: AnnouncementService, private route: ActivatedRoute) {

  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;
    this.announcementId = id;

    this.announceService.getAnnouncementById(id).subscribe({
      next: (res) => {
        this.announcement = res;
        this.announcementFoundSuccess = true;
        this.route.queryParams.subscribe(params => {
          window.scrollTo(0, 0);
          if (params['page']) {
            this.currentPage = +params['page'];
          }
          else{
            this.currentPage = 1;
          }

          this.loadResponses();
        });
      },
      error: (err) => {
        this.announcementFoundSuccess = false;
      }
    });

    this.loadResponses();
  }

  loadResponses() {
    this.responsesLoaded = false;
    this.responsesFound = undefined;
    this.announceService.getResponses(this.announcementId, this.currentPage, 10, this.prompt).subscribe({
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
}
