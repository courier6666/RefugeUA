import { Component, OnInit } from '@angular/core';
import { BaseVolunteerComponent } from '../base-volunteer-component/base-volunteer-component';
import { Params, Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { VolunteerGroup } from '../../../core/models';
import { mockVolunteerGroups } from '../../../shared/util/mock-up-data/volunteer-group-ua';
import { AuthService } from '../../../core/services/auth/auth-service';
import { VolunteerGroupsQuery } from '../../../core/queries/volunteer-groups-query';
import { VolunteerGroupService } from '../../../core/services/volunteer/volunteer-group-services';
import { Roles } from '../../../core/constants/user-roles-constants';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-volunteer-groups',
  standalone: false,
  templateUrl: './volunteer-groups.component.html',
  styleUrl: './volunteer-groups.component.css'
})
export class VolunteerGroupsComponent extends BaseVolunteerComponent implements OnInit {
  currentVolunteerGroups!: VolunteerGroup[];
  subscription!: Subscription;

  constructor(router: Router, route: ActivatedRoute, authService: AuthService, private volunteerGroupService: VolunteerGroupService) {
    super(router, route, authService);
  }

  protected override get activatedRoute(): ActivatedRoute {
    return this.route;
  }

  ngOnInit(): void {
    this.dataLoaded = true

    this.initializeDataFromQuery(this.route.snapshot.queryParams);

    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      this.initializeDataFromQuery(params);
      this.loadDataWithQuery(params);
    });
  }

  onFormsDataChanged(): void {

  }

  initializeForm() {

  }


  initializeDataFromQuery(params: Params): void {
    if (params['prompt']) {
      this.prompt = params['prompt'];
    }
  }

  override loadDataWithQuery(params: Params): void {
    this.dataLoaded = false;
    let volunteerGroupQuery: VolunteerGroupsQuery = { pageLength: 7, page: this.currentPage };

    if (params['prompt']) {
      volunteerGroupQuery.prompt = params['prompt'];
    }

    if (params['page']) {
      volunteerGroupQuery.page = +params['page'];
    }

    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.subscription = this.volunteerGroupService.getVolunteerGroups(volunteerGroupQuery).subscribe({
      next: (result) => {
        console.log(result);
        this.currentVolunteerGroups = result.items;
        this.pages = result.pagesCount;
        this.dataLoaded = true;
        this.dataFound = true;
      },
      error: (err) => {
        this.dataLoaded = true;
        this.dataFound = false;
        console.log(err);
      }
    });
  }
}
