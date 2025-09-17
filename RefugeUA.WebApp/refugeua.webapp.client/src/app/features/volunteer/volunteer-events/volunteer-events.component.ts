import { Component, OnInit } from '@angular/core';
import { BaseVolunteerComponent } from '../base-volunteer-component/base-volunteer-component';
import { Params, Router, ActivatedRoute } from '@angular/router';
import { VolunteerEvent, VolunteerGroup } from '../../../core/models';
import { mockVolunteerEvents } from '../../../shared/util/mock-up-data/volunteer-events-ua';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../../../core/services/auth/auth-service';
import { VolunteerEventType } from '../../../core/enums/volunteer-event-type-enum';
import { VolunteerEventService } from '../../../core/services/volunteer/volunteer-event-service';
import { VolunteerGroupService } from '../../../core/services/volunteer/volunteer-group-services';
import { IsValueNullNanOrUndefined } from '../../../shared/util/value-not-null-undefined-or-nan-checker';
import { VolunteerEventsQuery } from '../../../core/queries/volunteer-events-query';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-volunteer-events',
  standalone: false,
  templateUrl: './volunteer-events.component.html',
  styleUrl: './volunteer-events.component.css'
})
export class VolunteerEventsComponent extends BaseVolunteerComponent implements OnInit {

  volunteerEventSearchForm: FormGroup = new FormGroup([]);
  currentVolunteerEvents: VolunteerEvent[] = mockVolunteerEvents;
  public volunteerGroups!: VolunteerGroup[];
  subscription!: Subscription;

  constructor(router: Router, route: ActivatedRoute,
    authService: AuthService,
    private volunteerEventService: VolunteerEventService,
    private volunteerGroupService: VolunteerGroupService) {
    super(router, route, authService);
  }

  protected override get activatedRoute(): ActivatedRoute {
    return this.route;
  }

  ngOnInit(): void {


    this.initializeForm();

      this.volunteerGroupService.getVolunteerGroupPreviews().subscribe({
      next: (res) => {
        this.volunteerGroups = res;
        this.initializeDataFromQuery(this.route.snapshot.queryParams);
      },
      error: (err) => console.log(err)
    });

    this.initializeDataFromQuery(this.route.snapshot.queryParams);

    this.volunteerEventSearchForm.valueChanges.subscribe(() => {
      this.onFormsDataChanged();
    });

    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      if (this.volunteerEventSearchForm) {
        this.initializeDataFromQuery(params);
      }

      this.loadDataWithQuery(params);
    });
  }

  loadDataWithQuery(params: Params): void {
    this.dataLoaded = false;
    let volunteerEventQuery: VolunteerEventsQuery = { pageLength: 7, page: this.currentPage, isClosed: false };

    if (params['startDate']) {
      volunteerEventQuery.startDate = params['startDate'];
    }

    if (params['endDate']) {
      volunteerEventQuery.endDate = params['endDate'];
    }

    if (params['eventType']) {
      let eventType: VolunteerEventType = +params['eventType'];
      if (eventType == VolunteerEventType.Donation) {
        volunteerEventQuery.eventType = 'Donation';
      }
      else {
        volunteerEventQuery.eventType = 'Participation';
      }
    }

    if (params['volunteerGroupId']) {
      volunteerEventQuery.volunteerGroupId = +params['volunteerGroupId'];
    }

    if (params['page']) {
      volunteerEventQuery.page = +params['page'];
    }

    if (params['district']) {
      volunteerEventQuery.district = params['district'];
    }

    if (params['prompt']) {
      volunteerEventQuery.prompt = params['prompt'];
    }

    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.subscription = this.volunteerEventService.getVolunteerEvents(volunteerEventQuery).subscribe({
      next: (result) => {
        console.log(result);
        this.currentVolunteerEvents = result.items;
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

  onFormsDataChanged(): void {
    let formValue = this.volunteerEventSearchForm.value;
    let queryParams: any = {};

    if (!IsValueNullNanOrUndefined(formValue.startDate)) {
      queryParams.startDate = formValue.startDate;
    }

    if (!IsValueNullNanOrUndefined(formValue.endDate)) {
      queryParams.endDate = formValue.endDate;
    }

    if (!IsValueNullNanOrUndefined(formValue.eventType)) {
      queryParams.eventType = formValue.eventType;
    }

    if (!IsValueNullNanOrUndefined(formValue.volunteerGroupId)) {
      queryParams.volunteerGroupId = formValue.volunteerGroupId;
    }

    if (!IsValueNullNanOrUndefined(formValue.district) && formValue.district.trim() != "") {
      queryParams.district = formValue.district.trim();
    }

    queryParams.page = 1;

    if (this.prompt) {
      queryParams.prompt = this.prompt;
    }

    this.isDisplayedFull = false;
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: queryParams,
      queryParamsHandling: ''
    });
  }

  initializeForm() {
    this.volunteerEventSearchForm = new FormGroup({
      startDate: new FormControl<string | null>(null),
      endDate: new FormControl<string | null>(null),
      eventType: new FormControl<VolunteerEventType | null>(null),
      volunteerGroupId: new FormControl<number | null>(null),
      district: new FormControl<string | null>(null),
    });
  }


  initializeDataFromQuery(params: Params): void {
    if (!this.volunteerEventSearchForm) {
      console.error('Form is not initialized!');
      return;
    }

    if (params['startDate']) {
      this.volunteerEventSearchForm.get('startDate')?.setValue(params['startDate'], { emitEvent: false });
    }

    if (params['endDate']) {
      this.volunteerEventSearchForm.get('endDate')?.setValue(params['endDate'], { emitEvent: false });
    }

    if (params['eventType']) {
      this.volunteerEventSearchForm.get('eventType')?.setValue(+params['eventType'], { emitEvent: false });
    }

    if (params['volunteerGroupId']) {
      this.volunteerEventSearchForm.get('volunteerGroupId')?.setValue(+params['volunteerGroupId'], { emitEvent: false });
    }

    if (params['page']) {
      this.currentPage = +params['page'];
    }

    if (params['district']) {
      this.volunteerEventSearchForm.get('district')?.setValue(params['district'], { emitEvent: false });
    }

    if (params['prompt']) {
      this.prompt = params['prompt'];
    }

    this.volunteerEventSearchForm.markAsDirty();
  }

  get startDate() {
    return this.volunteerEventSearchForm.get('startDate');
  }

  get endDate() {
    return this.volunteerEventSearchForm.get('endDate');
  }

  get volunteerGroupId() {
    return this.volunteerEventSearchForm.get('volunteerGroupdId');
  }
}
