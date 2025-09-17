import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, Params, Router } from '@angular/router';
import { AnnouncementWorkService } from '../../../core/services/announcements/work/announcement-work-service';
import { WorkCategory } from '../../../core/models/work-category';
import { IsValueNullNanOrUndefined } from '../../../shared/util/value-not-null-undefined-or-nan-checker';
import { WorkAnnouncement } from '../../../core/models';
import { WorkAnnouncementQuery } from '../../../core/queries/work-announcement-query';
import { BaseAnnouncementComponent } from '../base-announcement/base-announcement.component';
import { filter } from 'rxjs';
import { SalaryOption, salaryOptionsLower, salaryOptionsUpper } from '../../../core/constants/salary-options';
import { AuthService } from '../../../core/services/auth/auth-service';
import { Subscription } from 'rxjs';
import { ViewChild, ElementRef } from '@angular/core';

@Component({
  selector: 'app-announcement-work',
  standalone: false,
  templateUrl: './announcement-work.component.html',
  styleUrl: './announcement-work.component.css'
})
export class AnnouncementWorkComponent extends BaseAnnouncementComponent implements OnInit {
  categories: WorkCategory[] = [];
  displayedCategories: WorkCategory[] = [];
  currentWorkAnnouncements: WorkAnnouncement[] = [];
  @ViewChild('groupsDropdown') groupsDropdownRef!: ElementRef<HTMLDivElement>;

  currentlyAvailableSalaryOptionsLower: SalaryOption[] = [...salaryOptionsLower];
  currentlyAvailableSalaryOptionsUpper: SalaryOption[] = [...salaryOptionsUpper];

  announcementWorkSearchForm: FormGroup = new FormGroup({});
  announcementsSubscription!: Subscription;


  constructor(router: Router, route: ActivatedRoute, authService: AuthService, private announceWorkService: AnnouncementWorkService) {
    super(router, route, authService);
  }

  ngOnInit(): void {
    this.announceWorkService.getCategories().subscribe({
      next: (data) => {
        this.categories = data;
        this.updateDisplayedCategories();
      },
      error: (error) => console.error('Failed to load work categories.', error),
    });

    this.announceWorkService.getAnnouncementGroups().subscribe({
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

    this.announcementWorkSearchForm.valueChanges.subscribe(() => {
      this.onFormsDataChanged();
      this.updateDisplayedCategories();
    });

    this.group?.valueChanges.subscribe(val => {
      this.setDisplayedGroups(val);
    });

    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      if (this.announcementWorkSearchForm) {
        this.initializeDataFromQuery(params);
      }

      this.loadAnnouncementsWithQuery(params);
    });
  }

  protected override get activatedRoute(): ActivatedRoute {
    return this.route;
  }

  get selectedCategories(): FormArray<FormControl<number>> {
    return this.announcementWorkSearchForm.get('jobCategories') as FormArray<FormControl<number>>;
  }

  override get group(): AbstractControl<any, any> | null {
    return this.announcementWorkSearchForm.get('announcementGroup');
  }

  onGroupsControlFocus() {
    this.groupsDropdownRef.nativeElement.style.display = "block";
  }

  onGroupsControlBlur() {
    this.groupsDropdownRef.nativeElement.style.display = "none";
  }

  initializeForm() {
    this.announcementWorkSearchForm = new FormGroup({
      salaryLower: new FormControl<number | null>(null),
      salaryUpper: new FormControl<number | null>(null),
      salaryNotSet: new FormControl<boolean>(false),
      jobCategories: new FormArray<FormControl<number>>([]),
      district: new FormControl<string | null>(null),
      announcementGroup: new FormControl<string | null>(null),
    });
  }

  initializeDataFromQuery(params: Params) {
    if (!this.announcementWorkSearchForm) {
      console.error('Form is not initialized!');
      return;
    }

    if (params['salaryLower'] && !Number.isNaN(+params['salaryLower'])) {
      this.announcementWorkSearchForm.get('salaryLower')?.setValue(+params['salaryLower'], { emitEvent: false });
    }
    else {
      this.announcementWorkSearchForm.get('salaryLower')?.setValue(null, { emitEvent: false });
    }

    if (params['salaryUpper'] && !Number.isNaN(+params['salaryUpper'])) {
      this.announcementWorkSearchForm.get('salaryUpper')?.setValue(+params['salaryUpper'], { emitEvent: false });
    }
    else {
      this.announcementWorkSearchForm.get('salaryUpper')?.setValue(null, { emitEvent: false });
    }

    if (params['salaryNotSet']) {
      this.announcementWorkSearchForm.get('salaryNotSet')?.setValue(params['salaryNotSet'] === 'true', { emitEvent: false });
    }

    if (params['page']) {
      this.currentPage = +params['page'];
    }

    if (params['district']) {
      this.announcementWorkSearchForm.get('district')?.setValue(params['district'], { emitEvent: false });
    }

    if (params['announcementGroup']) {
      this.announcementWorkSearchForm.get('announcementGroup')?.setValue(params['announcementGroup'], { emitEvent: false });
    }

    if (params['prompt']) {
      this.prompt = params['prompt'];
    }

    let ids = [] as number[];
    if (params['jobCategories']) {
      ids = Array.from(new Set((params['jobCategories'] as string).split('+').map(s => +s)));
      const controls = ids.map(id => new FormControl<number>(id, { nonNullable: true }));
      const newArray = new FormArray(controls, { updateOn: 'change' });

      this.announcementWorkSearchForm.setControl('selectedCategories', newArray, { emitEvent: false });
    }

    this.updateDisplayedCategories();
    this.updateAvailableSalaryOptions();
    this.announcementWorkSearchForm.markAsDirty();
  }

  onFormsDataChanged(): void {

    let formValue = this.announcementWorkSearchForm.value;
    let queryParams: any = {};

    if (!IsValueNullNanOrUndefined(formValue.salaryLower)) {
      queryParams.salaryLower = formValue.salaryLower;
    }

    if (!IsValueNullNanOrUndefined(formValue.salaryUpper)) {
      queryParams.salaryUpper = formValue.salaryUpper;
    }

    if (!IsValueNullNanOrUndefined(formValue.salaryNotSet)) {
      queryParams.salaryNotSet = formValue.salaryNotSet;
    }

    let jobCategories = Array.from(new Set(formValue.jobCategories)).join('+');

    if (!IsValueNullNanOrUndefined(formValue.jobCategories) && (jobCategories?.length ?? 0) > 0) {
      queryParams.jobCategories = jobCategories;
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

  loadAnnouncementsWithQuery(params: Params) {
    this.announcementsLoaded = false;
    let workAnnouncementQuery: WorkAnnouncementQuery = { pageLength: 7, page: 1, isClosed: false };

    if (params['salaryLower'] && !Number.isNaN(+params['salaryLower'])) {
      workAnnouncementQuery.salaryLower = +params['salaryLower'];
    }

    if (params['salaryUpper'] && !Number.isNaN(+params['salaryUpper'])) {
      workAnnouncementQuery.salaryUpper = +params['salaryUpper'];
    }

    if (params['salaryNotSet']) {
      workAnnouncementQuery.salaryNotSet = params['salaryNotSet'] === 'true';
    }

    if (params['page']) {
      workAnnouncementQuery.page = +params['page'];
    }

    if (params['district']) {
      workAnnouncementQuery.district = params['district'];
    }

    if (params['announcementGroup']) {
      workAnnouncementQuery.announcementGroup = params['announcementGroup'];
    }

    if (params['prompt']) {
      workAnnouncementQuery.prompt = params['prompt'];
    }

    let ids = [] as number[];
    if (params['jobCategories']) {
      ids = Array.from(new Set((params['jobCategories'] as string).split('+').map(s => +s)));
    }

    if (ids.length > 0) {
      workAnnouncementQuery.jobCategories = ids;
    }

    if (this.announcementsSubscription) {
      this.announcementsSubscription.unsubscribe();
    }

    this.announcementsSubscription = this.announceWorkService.getWorkAnnouncements(workAnnouncementQuery).subscribe({
      next: (result) => {
        console.log(result);
        this.currentWorkAnnouncements = result.items;
        this.pages = result.pagesCount;
        this.announcementsLoaded = true;
        this.announcementsFound = true;
      },
      error: (err) => {
        this.announcementsFound = false;
        this.announcementsLoaded = true;
        console.log(err);
      }
    });
  }

  onAllCategoriesButtonClick() {
    this.isDisplayedFull = true;
  }

  onCheckboxChange(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = +input.value;

    if (input.checked) {
      this.selectedCategories.push(new FormControl<number>(value, { nonNullable: true }));
    } else {
      const index = this.selectedCategories.controls.findIndex(c => c.value === value);
      if (index !== -1) this.selectedCategories.removeAt(index);
    }
  }

  isCategorySelected(id: number): boolean {
    return this.selectedCategories.value.includes(id);
  }

  updateDisplayedCategories() {
    this.displayedCategories = [];
    let ids = this.selectedCategories.value;
    this.displayedCategories.push(...this.categories.filter(c => ids.includes(c.id!)));
    this.displayedCategories.push(...this.categories.filter(c => !ids.includes(c.id!)));
  }

  updateAvailableSalaryOptions() {
    var formValue = this.announcementWorkSearchForm.value;
    if (formValue.salaryUpper == null) {
      this.currentlyAvailableSalaryOptionsLower = [...salaryOptionsLower];
    }
    else {
      this.currentlyAvailableSalaryOptionsLower = salaryOptionsLower.filter(o => o.value <= formValue.salaryUpper);
    }

    if (formValue.salaryLower == null) {
      this.currentlyAvailableSalaryOptionsUpper = [...salaryOptionsUpper];
    }
    else {
      this.currentlyAvailableSalaryOptionsUpper = salaryOptionsUpper.filter(o => o.value >= formValue.salaryLower);
    }
  }
}
