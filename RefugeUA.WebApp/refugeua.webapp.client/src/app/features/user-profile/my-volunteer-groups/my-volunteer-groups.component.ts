import { Component } from '@angular/core';
import { VolunteerGroup } from '../../../core/models';
import { VolunteerGroupService } from '../../../core/services/volunteer/volunteer-group-services';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-my-volunteer-groups',
  standalone: false,
  templateUrl: './my-volunteer-groups.component.html',
  styleUrl: './my-volunteer-groups.component.css'
})
export class MyVolunteerGroupsComponent {
  volunteerGroups: VolunteerGroup[] = [];
  currentPage: number = 1;
  volunteerGroupsLoaded = false;
  volunteerGroupsFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  subscription!: Subscription;

  constructor(private volunteerGroupService: VolunteerGroupService, private router: Router, private route: ActivatedRoute) {

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

      this.loadVolunteerGroups();
    });
  }

  getNavigationCallback(): (page: number) => void {
    return (page: number) => this.onPageNavigation(page);
  }

  getOnSearchBarCallback(): (prompt: string | null) => void {
    return (prompt: string | null) => {
      this.prompt = prompt;
      this.currentPage = 1;
      this.loadVolunteerGroups();
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

  loadVolunteerGroups() {

    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.volunteerGroupsLoaded = false;
    this.volunteerGroupsFound = undefined;
    this.subscription = this.volunteerGroupService.getMyVolunteerGroups(this.currentPage, 10, this.prompt).subscribe({
      next: (response) => {
        this.volunteerGroups = response.items;
        this.pages = response.pagesCount;
        this.volunteerGroupsFound = response.totalCount > 0;
        this.volunteerGroupsLoaded = true;
      },
      error: (error) => {
        this.volunteerGroupsLoaded = true;
        this.volunteerGroupsFound = false;
      }
    });
  }
}
