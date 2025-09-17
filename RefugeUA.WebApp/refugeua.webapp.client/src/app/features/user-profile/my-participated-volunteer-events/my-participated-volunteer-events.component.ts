import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { VolunteerEventService } from '../../../core/services/volunteer/volunteer-event-service';
import { VolunteerEvent } from '../../../core/models';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-my-participated-volunteer-events',
  standalone: false,
  templateUrl: './my-participated-volunteer-events.component.html',
  styleUrl: './my-participated-volunteer-events.component.css'
})
export class MyParticipatedVolunteerEventsComponent implements OnInit {
  volunteerEvents: VolunteerEvent[] = [];
  currentPage: number = 1;
  volunteerEventsLoaded = false;
  volunteerEventsFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  subscription!: Subscription;

  constructor(private volunteerEventService: VolunteerEventService, private router: Router, private route: ActivatedRoute) {

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

      this.loadVolunteerEvents();
    });

  }

  getNavigationCallback(): (page: number) => void {
    return (page: number) => this.onPageNavigation(page);
  }

  getOnSearchBarCallback(): (prompt: string | null) => void {
    return (prompt: string | null) => {
      this.prompt = prompt;
      this.currentPage = 1;
      this.loadVolunteerEvents();
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

  loadVolunteerEvents() {

    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.volunteerEventsLoaded = false;
    this.volunteerEventsFound = undefined;
    this.subscription = this.volunteerEventService.getParticipatedVolunteerEvents(this.currentPage, 10, this.prompt).subscribe({
      next: (response) => {
        this.volunteerEvents = response.items;
        this.pages = response.pagesCount;
        this.volunteerEventsFound = response.totalCount > 0;
        this.volunteerEventsLoaded = true;
      },
      error: (error) => {
        this.volunteerEventsLoaded = true;
        this.volunteerEventsFound = false;
      }
    });
  }
}
