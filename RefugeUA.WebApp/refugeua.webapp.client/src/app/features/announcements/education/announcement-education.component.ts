import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AnnouncementEducationService } from '../../../core/services/announcements/education/announcement-education-service';
import { IsValueNullNanOrUndefined } from '../../../shared/util/value-not-null-undefined-or-nan-checker';
import { EducationAnnouncement } from '../../../core/models';
import { EducationAnnouncementQuery } from '../../../core/queries/education-announcement-query';
import { BaseAnnouncementComponent } from '../base-announcement/base-announcement.component';
import { TimeSpan } from '../../../shared/util/time-span';
import { EducationType } from '../../../core/frontend-only-models/education-type';
import { TargetGroup } from '../../../core/frontend-only-models/target-group';
import { EducationFee, educationFeesLower, educationFeesUpper } from '../../../core/constants/education-fees';
import { EducationDuration, educationDurationsLower, educationDurationsUpper } from '../../../core/constants/education-durations';
import { AuthService } from '../../../core/services/auth/auth-service';
import { ActivatedRouteSnapshot } from '@angular/router';
import { debounceTime, distinctUntilChanged, Subscription } from 'rxjs';

@Component({
  selector: 'app-announcement-education',
  standalone: false,
  templateUrl: './announcement-education.component.html',
  styleUrl: './announcement-education.component.css',
})

export class AnnouncementEducationComponent extends BaseAnnouncementComponent implements OnInit {

  announcementEducationSearchForm: FormGroup = new FormGroup({});
  currentEducationAnnouncements: EducationAnnouncement[] = [];

  educationTypes: EducationType[] = [];
  targetGroups: TargetGroup[] = [];

  displayedEducationTypes: EducationType[] = [];
  displayedTargetGroups: TargetGroup[] = [];

  currentlyAvailableEducationFeesLower: EducationFee[] = [];
  currentlyAvailableEducationFeesUpper: EducationFee[] = [];

  currentlyAvailableEducationDurationsLower: EducationDuration[] = [];
  currentlyAvailableEducationDurationsUpper: EducationDuration[] = [];

  @ViewChild('groupsDropdown') groupsDropdownRef!: ElementRef<HTMLDivElement>;

  announcementsSubscription!: Subscription;

  constructor(router: Router, route: ActivatedRoute, authService: AuthService, private announcementEducationService: AnnouncementEducationService) {
    super(router, route, authService);
  }

  ngOnInit(): void {
    this.announcementEducationService.getEducationTypes().subscribe({
      next: (result) => this.educationTypes = result,
      error: (error) => console.error('Failed to load education types.', error)
    });

    this.announcementEducationService.getTargetGroups().subscribe({
      next: (result) => this.targetGroups = result,
      error: (error) => console.error('Failed to load target groups.', error)
    });

    this.announcementEducationService.getAnnouncementGroups().subscribe({
      next: (res) => {
        this.groups = res;
        this.displayedGroups = res;
        if (this.group) {
          this.setDisplayedGroups(this.group.value);
        }
      },
      error: (err) => {
        console.log(err);
      }
    });

    this.initializeForm();

    this.initializeDataFromQuery(this.route.snapshot.queryParams);

    this.announcementEducationSearchForm.valueChanges
      .subscribe(() => {
        this.onFormsDataChanged();
        this.updateDisplayedEducationTypes();
        this.updateDisplayedTargetGroups();
      });

    this.group?.valueChanges.subscribe(val => {
      this.setDisplayedGroups(val);
    });


    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      if (this.announcementEducationSearchForm) {
        this.initializeDataFromQuery(params);
      }

      this.loadAnnouncementsWithQuery(params);
    });
  }

  onGroupsControlFocus() {
    this.groupsDropdownRef.nativeElement.style.display = "block";
  }

  onGroupsControlBlur() {
    this.groupsDropdownRef.nativeElement.style.display = "none";
  }

  override get group(): AbstractControl<any, any> | null {
    return this.announcementEducationSearchForm.get('announcementGroup');
  }

  protected override get activatedRoute(): ActivatedRoute {
    return this.route;
  }

  get selectedEducationTypes(): FormArray<FormControl<number>> {
    return this.announcementEducationSearchForm.get('educationTypes') as FormArray<FormControl<number>>;
  }

  isEducationTypeSelected(id: number): boolean {
    return this.selectedEducationTypes.value.includes(id);
  }

  get selectedTargetGroups(): FormArray<FormControl<number>> {
    return this.announcementEducationSearchForm.get('targetGroups') as FormArray<FormControl<number>>;
  }

  isTargetGroupSelected(id: number): boolean {
    return this.selectedTargetGroups.value.includes(id);
  }

  loadAnnouncementsWithQuery(params: Params): void {
    this.announcementsLoaded = false;
    let educationAnnouncementQuery: EducationAnnouncementQuery = { pageLength: 7, page: 1 };

    if (params['feeLower'] && !Number.isNaN(+params['feeLower'])) {
      educationAnnouncementQuery.feeLower = +params['feeLower'];
    }

    if (params['feeUpper'] && !Number.isNaN(+params['feeUpper'])) {
      educationAnnouncementQuery.feeUpper = +params['feeUpper'];
    }

    if (params['isFreeOnly']) {
      educationAnnouncementQuery.isFreeOnly = params['isFreeOnly'] === 'true';
    }

    if (params['durationLower'] && !Number.isNaN(+params['durationLower'])) {
      educationAnnouncementQuery.durationLower = +params['durationLower'];
    }

    if (params['durationUpper'] && !Number.isNaN(+params['durationUpper'])) {
      educationAnnouncementQuery.durationUpper = +params['durationUpper'];
    }

    if (params['page']) {
      educationAnnouncementQuery.page = +params['page'];
    }

    if (params['prompt']) {
      educationAnnouncementQuery.prompt = params['prompt'];
    }

    if (params['district']) {
      educationAnnouncementQuery.district = params['district'];
    }

    if (params['announcementGroup']) {
      educationAnnouncementQuery.announcementGroup = params['announcementGroup'];
    }

    let educationTypesIds = [] as number[];
    if (params['educationTypes']) {
      educationTypesIds = Array.from(new Set((params['educationTypes'] as string).split('+').map(s => +s)));
    }

    let targetGroupsIds = [] as number[];
    if (params['targetGroups']) {
      targetGroupsIds = Array.from(new Set((params['targetGroups'] as string).split('+').map(s => +s)));
    }

    if (educationTypesIds.length > 0) {
      educationAnnouncementQuery.educationTypes = this.educationTypes.filter(e => educationTypesIds.includes(e.id)).map(e => e.name);
    }

    if (targetGroupsIds.length > 0) {
      educationAnnouncementQuery.targetGroups = this.targetGroups.filter(g => targetGroupsIds.includes(g.id)).map(g => g.name);
    }

    if (this.announcementsSubscription) {
      this.announcementsSubscription.unsubscribe();
    }

    this.announcementsSubscription = this.announcementEducationService.getEducationAnnouncements(educationAnnouncementQuery).subscribe({
      next: (result) => {
        console.log(result);
        this.currentEducationAnnouncements = result.items;
        this.pages = result.pagesCount
        this.announcementsLoaded = true;
        this.announcementsFound = result.items.length > 0;
      },
      error: (err) => {
        console.log(err);
        this.announcementsFound = false;
        this.announcementsLoaded = true;
        console.log(err);
      }
    });
  }

  onFormsDataChanged(): void {
    let formValue = this.announcementEducationSearchForm.value;
    let queryParams: any = {};

    if (!IsValueNullNanOrUndefined(formValue.feeLower)) {
      queryParams.feeLower = formValue.feeLower;
    }

    if (!IsValueNullNanOrUndefined(formValue.feeUpper)) {
      queryParams.feeUpper = formValue.feeUpper;
    }

    if (!IsValueNullNanOrUndefined(formValue.isFreeOnly)) {
      queryParams.isFreeOnly = formValue.isFreeOnly;
    }

    if (!IsValueNullNanOrUndefined(formValue.durationLower)) {
      queryParams.durationLower = formValue.durationLower;
    }

    if (!IsValueNullNanOrUndefined(formValue.durationUpper)) {
      queryParams.durationUpper = formValue.durationUpper;
    }

    let educationTypes = Array.from(new Set(formValue.educationTypes)).join('+');

    if (!IsValueNullNanOrUndefined(formValue.educationTypes) && (educationTypes?.length ?? 0) > 0) {
      queryParams.educationTypes = educationTypes;
    }

    let targetGroups = Array.from(new Set(formValue.targetGroups)).join('+');

    if (!IsValueNullNanOrUndefined(formValue.targetGroups) && (targetGroups?.length ?? 0) > 0) {
      queryParams.targetGroups = targetGroups;
    }

    if (!IsValueNullNanOrUndefined(formValue.district) && formValue.district.trim() != "") {
      queryParams.district = formValue.district.trim();
    }

    if (!IsValueNullNanOrUndefined(formValue.announcementGroup) && formValue.announcementGroup.trim() != "") {
      queryParams.announcementGroup = formValue.announcementGroup.trim();
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

  initializeForm(): void {
    this.announcementEducationSearchForm = new FormGroup({
      feeLower: new FormControl<number | null>(null),
      feeUpper: new FormControl<number | null>(null),
      isFreeOnly: new FormControl<boolean>(false),
      durationLower: new FormControl<number | null>(null),
      durationUpper: new FormControl<number | null>(null),
      educationTypes: new FormArray<FormControl<string>>([]),
      targetGroups: new FormArray<FormControl<string>>([]),
      district: new FormControl<string | null>(null),
      announcementGroup: new FormControl<string | null>(null),
    });
  }

  initializeDataFromQuery(params: Params): void {

    if (!this.announcementEducationSearchForm) {
      console.error('Form is not initialized!');
      return;
    }

    if (params['feeLower'] && !Number.isNaN(+params['feeLower'])) {
      this.announcementEducationSearchForm.get('feeLower')?.setValue(+params['feeLower'], { emitEvent: false });
    }
    else {
      this.announcementEducationSearchForm.get('feeLower')?.setValue(null, { emitEvent: false });
    }

    if (params['feeUpper'] && !Number.isNaN(+params['feeUpper'])) {
      this.announcementEducationSearchForm.get('feeUpper')?.setValue(+params['feeUpper'], { emitEvent: false });
    }
    else {
      this.announcementEducationSearchForm.get('feeUpper')?.setValue(null, { emitEvent: false });
    }

    if (params['isFreeOnly']) {
      this.announcementEducationSearchForm.get('isFreeOnly')?.setValue(params['isFreeOnly'] === 'true', { emitEvent: false });
    }

    if (params['durationLower'] && !Number.isNaN(+params['durationLower'])) {
      this.announcementEducationSearchForm.get('durationLower')?.setValue(+params['durationLower'], { emitEvent: false });
    }
    else {
      this.announcementEducationSearchForm.get('durationLower')?.setValue(null, { emitEvent: false });
    }

    if (params['durationUpper'] && !Number.isNaN(+params['durationUpper'])) {
      this.announcementEducationSearchForm.get('durationUpper')?.setValue(+params['durationUpper'], { emitEvent: false });
    }
    else {
      this.announcementEducationSearchForm.get('durationUpper')?.setValue(null, { emitEvent: false });
    }

    if (params['page']) {
      this.currentPage = +params['page'];
    }

    if (params['prompt']) {
      this.prompt = params['prompt'];
    }

    if (params['district']) {
      this.announcementEducationSearchForm.get('district')?.setValue(params['district'], { emitEvent: false });
    }

    if (params['announcementGroup']) {
      this.announcementEducationSearchForm.get('announcementGroup')?.setValue(params['announcementGroup'], { emitEvent: false });
    }

    let educationTypesIds = [] as number[];
    if (params['educationTypes']) {
      educationTypesIds = Array.from(new Set((params['educationTypes'] as string).split('+').map(s => +s)));
      const controls = educationTypesIds.map(id => new FormControl<number>(id, { nonNullable: true }));
      const newArray = new FormArray(controls, { updateOn: 'change' });

      this.announcementEducationSearchForm.setControl('educationTypes', newArray, { emitEvent: false });
    }

    let targetGroupsIds = [] as number[];
    if (params['targetGroups']) {
      targetGroupsIds = Array.from(new Set((params['targetGroups'] as string).split('+').map(s => +s)));
      const controls = targetGroupsIds.map(id => new FormControl<number>(id, { nonNullable: true }));
      const newArray = new FormArray(controls, { updateOn: 'change' });

      this.announcementEducationSearchForm.setControl('targetGroups', newArray, { emitEvent: false });
    }

    this.updateDisplayedEducationTypes();
    this.updateDisplayedTargetGroups();
    this.updateAvailableEducationFees();
    this.updateAvailableEducationDurations();
    this.announcementEducationSearchForm.markAsDirty();
  }

  onEducationTypeCheckboxChanged(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = +input.value;

    if (input.checked) {
      this.selectedEducationTypes.push(new FormControl<number>(value, { nonNullable: true }));
    } else {
      const index = this.selectedEducationTypes.controls.findIndex(c => c.value === value);
      if (index !== -1) this.selectedEducationTypes.removeAt(index);
    }
  }

  onTargetGroupCheckboxChanged(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = +input.value;

    console.log(1);

    if (input.checked) {
      this.selectedTargetGroups.push(new FormControl<number>(value, { nonNullable: true }));
    } else {
      const index = this.selectedTargetGroups.controls.findIndex(c => c.value === value);
      if (index !== -1) this.selectedTargetGroups.removeAt(index);
    }

    console.log(this.selectedTargetGroups.value);
  }

  updateDisplayedEducationTypes() {
    this.displayedEducationTypes = [];
    let ids = this.selectedEducationTypes.value;
    this.displayedEducationTypes.push(...this.educationTypes.filter(c => ids.includes(c.id)));
    this.displayedEducationTypes.push(...this.educationTypes.filter(c => !ids.includes(c.id)));
  }

  updateDisplayedTargetGroups() {
    this.displayedTargetGroups = [];
    let ids = this.selectedTargetGroups.value;
    this.displayedTargetGroups.push(...this.targetGroups.filter(c => ids.includes(c.id)));
    this.displayedTargetGroups.push(...this.targetGroups.filter(c => !ids.includes(c.id)));
  }

  updateAvailableEducationFees() {
    var formValue = this.announcementEducationSearchForm.value;
    if (formValue.feeUpper == null) {
      this.currentlyAvailableEducationFeesLower = [...educationFeesLower];
    }
    else {
      this.currentlyAvailableEducationFeesLower = educationFeesLower.filter(f => f.value < formValue.feeUpper);
    }

    if (formValue.feeLower == null) {
      this.currentlyAvailableEducationFeesUpper = [...educationFeesUpper];
    }
    else {
      this.currentlyAvailableEducationFeesUpper = educationFeesUpper.filter(f => f.value > formValue.feeLower);
    }
  }

  updateAvailableEducationDurations() {
    var formValue = this.announcementEducationSearchForm.value;

    if (formValue.durationUpper == null) {
      this.currentlyAvailableEducationDurationsLower = [...educationDurationsLower];
    }
    else {
      this.currentlyAvailableEducationDurationsLower = educationDurationsLower.filter(d => d.days < formValue.durationUpper);
    }

    if (formValue.durationLower == null) {
      this.currentlyAvailableEducationDurationsUpper = [...educationDurationsUpper];
    }
    else {
      this.currentlyAvailableEducationDurationsUpper = educationDurationsUpper.filter(d => d.days > formValue.durationLower);
    }
  }
}
