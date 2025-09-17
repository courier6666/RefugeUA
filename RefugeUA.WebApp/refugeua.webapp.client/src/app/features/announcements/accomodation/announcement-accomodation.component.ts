import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AnnouncementAccomodationService } from '../../../core/services/announcements/accomodation/announcement-accomodation-service';
import { IsValueNullNanOrUndefined } from '../../../shared/util/value-not-null-undefined-or-nan-checker';
import { BaseAnnouncementComponent } from '../base-announcement/base-announcement.component';
import { TimeSpan } from '../../../shared/util/time-span';
import { BuildingType, buildingTypes } from '../../../core/constants/building-type';
import { AccomodationAnnouncement } from '../../../core/models';
import { AreaSqMeters, areaSqMetersLower, areaSqMetersUpper } from '../../../core/constants/areq-sq-meters';
import { AccomodationCapacity, accomodationCapacities } from '../../../core/constants/accomodation-capacity';
import { AccomodationFloors, accomodationFloors } from '../../../core/constants/accomodation-floors';
import { AccomodationRooms, accomodationRooms } from '../../../core/constants/accomodation-rooms';
import { AccomodationAnnouncementQuery } from '../../../core/queries/accomodation-announcement-query';
import { AccomodationPrice, accomodationPriceOptionsLower, accomodationPriceOptionsUpper } from '../../../core/constants/accomodation-prices';
import { AuthService } from '../../../core/services/auth/auth-service';
import { ActivatedRouteSnapshot } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-announcement-accomodation',
  standalone: false,
  templateUrl: './announcement-accomodation.component.html',
  styleUrl: './announcement-accomodation.component.css'
})
export class AnnouncementAccomodationComponent extends BaseAnnouncementComponent implements OnInit {
  announcementAccomodationSearchForm: FormGroup = new FormGroup({});
  currentAccomodationAnnouncements: AccomodationAnnouncement[] = [];

  accomodationCapacity: AccomodationCapacity[] = [...accomodationCapacities];
  accomodationFloors: AccomodationFloors[] = [...accomodationFloors];
  accomodationRooms: AccomodationRooms[] = [...accomodationRooms];

  currentlyAvailableAreqSqMetersLower: AreaSqMeters[] = [];
  currentlyAvailableAreqSqMetersUpper: AreaSqMeters[] = [];

  buildingTypes: BuildingType[] = [];
  displayedBuildingTypes: BuildingType[] = [];

  currenltyAvailableAccomodationPricesLower: AccomodationPrice[] = [];
  currenltyAvailableAccomodationPricesUpper: AccomodationPrice[] = [];

  @ViewChild('groupsDropdown') groupsDropdownRef!: ElementRef<HTMLDivElement>;

  announcementsSubscription!: Subscription;

  constructor(router: Router, route: ActivatedRoute, authService: AuthService, public announcementAccomodationService: AnnouncementAccomodationService) {
    super(router, route, authService);
  }

  ngOnInit(): void {

    this.announcementAccomodationService.getBuildingTypes().subscribe({
      next: (result) => this.buildingTypes = result,
      error: (error) => console.error("Failed to load building types!", error),
    });

    this.announcementAccomodationService.getAnnouncementGroups().subscribe({
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

    this.announcementAccomodationSearchForm.valueChanges.subscribe(() => {
      this.onFormsDataChanged();
      this.updateDisplayedBuildingTypes();
      this.updateAvailableAreaSqMeters();
      this.updateAvailableAccomodationPrices();
    });

    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      if (this.announcementAccomodationSearchForm) {
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
    return this.announcementAccomodationSearchForm.get('announcementGroup');
  }

  protected override get activatedRoute(): ActivatedRoute {
    return this.route;
  }

  get selectedBuildingTypes(): FormArray<FormControl<number>> {
    return this.announcementAccomodationSearchForm.get('buildingTypes') as FormArray<FormControl<number>>;
  }

  isBuildingTypeSelected(id: number): boolean {
    return this.selectedBuildingTypes.value.includes(id);
  }

  loadAnnouncementsWithQuery(params: Params): void {
    this.announcementsLoaded = false;
    let accomodationAnnouncementQuery: AccomodationAnnouncementQuery = { pageLength: 7, page: 1 };

    if (params['capacity']) {
      accomodationAnnouncementQuery.capacity = +params['capacity'];
    }

    if (params['numberOfRooms']) {
      accomodationAnnouncementQuery.numberOfRooms = +params['numberOfRooms'];
    }

    if (params['floors']) {
      accomodationAnnouncementQuery.floors = +params['floors'];
    }

    if (params['areaSqMetersLower']) {
      accomodationAnnouncementQuery.areaSqMetersLower = +params['areaSqMetersLower'];
    }

    if (params['areaSqMetersUpper']) {
      accomodationAnnouncementQuery.areaSqMetersUpper = +params['areaSqMetersUpper'];
    }

    if (params['priceLower']) {
      accomodationAnnouncementQuery.priceLower = +params['priceLower'];
    }

    if (params['priceUpper']) {
      accomodationAnnouncementQuery.priceUpper = +params['priceUpper'];
    }

    if (params['isFree'] && params['isFree'] === 'true') {
      accomodationAnnouncementQuery.isFree = true;
    }

    if (params['petsAllowed'] && params['petsAllowed'] === 'true') {
      accomodationAnnouncementQuery.petsAllowed = true;
    }

    if (params['buildingTypes']) {
      let ids = params['buildingTypes'].split('+').map((c: string) => +c);
      accomodationAnnouncementQuery.buildingTypes = this.buildingTypes.filter(c => ids.includes(c.id)).map(c => c.name);
    }

    if (params['page']) {
      accomodationAnnouncementQuery.page = +params['page'];
    }

    if (params['prompt']) {
      accomodationAnnouncementQuery.prompt = params['prompt'];
    }

    if (params['district']) {
      accomodationAnnouncementQuery.district = params['district'];
    }

    if (params['announcementGroup']) {
      accomodationAnnouncementQuery.announcementGroup = params['announcementGroup'];
    }

    if (this.announcementsSubscription) {
      this.announcementsSubscription.unsubscribe();
    }

    this.announcementsSubscription = this.announcementAccomodationService.getAccomodationAnnouncements(accomodationAnnouncementQuery).subscribe({
      next: (result) => {
        this.currentAccomodationAnnouncements = result.items;
        this.pages = result.pagesCount
        this.announcementsLoaded = true;
        this.announcementsFound = result.items.length > 0;
      },
      error: (err) => {
        this.announcementsFound = false;
        this.announcementsLoaded = true;
      }
    });
  }

  onFormsDataChanged(): void {
    let formValue = this.announcementAccomodationSearchForm.value;
    let queryParams: any = {};

    if (!IsValueNullNanOrUndefined(formValue.capacity)) {
      queryParams.capacity = formValue.capacity;
    }

    if (!IsValueNullNanOrUndefined(formValue.numberOfRooms)) {
      queryParams.numberOfRooms = formValue.numberOfRooms;
    }

    if (!IsValueNullNanOrUndefined(formValue.floors)) {
      queryParams.floors = formValue.floors;
    }

    if (!IsValueNullNanOrUndefined(formValue.areaSqMetersLower)) {
      queryParams.areaSqMetersLower = formValue.areaSqMetersLower;
    }

    if (!IsValueNullNanOrUndefined(formValue.areaSqMetersUpper)) {
      queryParams.areaSqMetersUpper = formValue.areaSqMetersUpper;
    }

    if (!IsValueNullNanOrUndefined(formValue.priceLower)) {
      queryParams.priceLower = formValue.priceLower;
    }

    if (!IsValueNullNanOrUndefined(formValue.priceUpper)) {
      queryParams.priceUpper = formValue.priceUpper;
    }

    if (!IsValueNullNanOrUndefined(formValue.isFreeOnly) && formValue.isFreeOnly) {
      queryParams.isFreeOnly = formValue.isFreeOnly;
    }

    if (!IsValueNullNanOrUndefined(formValue.buildingTypes) && formValue.buildingTypes.length > 0) {
      let ids = formValue.buildingTypes;
      queryParams.buildingTypes = Array.from(new Set(ids)).join('+');
    }

    if (!IsValueNullNanOrUndefined(formValue.petsAllowed) && formValue.petsAllowed) {
      queryParams.petsAllowed = formValue.petsAllowed;
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
    this.announcementAccomodationSearchForm = new FormGroup({
      capacity: new FormControl<number | null>(null),
      numberOfRooms: new FormControl<number | null>(null),
      floors: new FormControl<number | null>(null),
      areaSqMetersLower: new FormControl<number | null>(null),
      areaSqMetersUpper: new FormControl<number | null>(null),
      priceLower: new FormControl<number | null>(null),
      priceUpper: new FormControl<number | null>(null),
      isFreeOnly: new FormControl<boolean>(false),
      buildingTypes: new FormArray<FormControl<number>>([]),
      petsAllowed: new FormControl<boolean>(false),
      district: new FormControl<string | null>(null),
      announcementGroup: new FormControl<string | null>(null),
    });
  }

  initializeDataFromQuery(params: Params): void {

    if (!this.announcementAccomodationSearchForm) {
      console.error('Form is not initialized!');
      return;
    }

    if (params['capacity']) {
      this.announcementAccomodationSearchForm.get('capacity')?.setValue(+params['capacity'], { emitEvent: false });
    }

    if (params['numberOfRooms']) {
      this.announcementAccomodationSearchForm.get('numberOfRooms')?.setValue(+params['numberOfRooms'], { emitEvent: false });
    }

    if (params['floors']) {
      this.announcementAccomodationSearchForm.get('floors')?.setValue(+params['floors'], { emitEvent: false });
    }

    if (params['areaSqMetersLower']) {
      this.announcementAccomodationSearchForm.get('areaSqMetersLower')?.setValue(+params['areaSqMetersLower'], { emitEvent: false });
    }

    if (params['areaSqMetersUpper']) {
      this.announcementAccomodationSearchForm.get('areaSqMetersUpper')?.setValue(+params['areaSqMetersUpper'], { emitEvent: false });
    }

    if (params['priceLower']) {
      this.announcementAccomodationSearchForm.get('priceLower')?.setValue(+params['priceLower'], { emitEvent: false });
    }

    if (params['priceUpper']) {
      this.announcementAccomodationSearchForm.get('priceUpper')?.setValue(+params['priceUpper'], { emitEvent: false });
    }

    if (params['isFree']) {
      this.announcementAccomodationSearchForm.get('isFreeOnly')?.setValue(params['isFree'] === 'true', { emitEvent: false });
    }

    if (params['petsAllowed']) {
      this.announcementAccomodationSearchForm.get('petsAllowed')?.setValue(params['petsAllowed'] === 'true', { emitEvent: false });
    }

    if (params['buildingTypes']) {
      let ids = params['buildingTypes'].split('+').map((c: string) => +c);
      this.selectedBuildingTypes.clear();
      ids.forEach((id: number) => {
        this.selectedBuildingTypes.push(new FormControl<number>(id, { nonNullable: true }));
      });
    }

    if (params['page']) {
      this.currentPage = +params['page'];
    }

    if (params['prompt']) {
      this.prompt = params['prompt'];
    }

    if (params['district']) {
      this.announcementAccomodationSearchForm.get('district')?.setValue(params['district'], { emitEvent: false });
    }

    if (params['announcementGroup']) {
      this.announcementAccomodationSearchForm.get('announcementGroup')?.setValue(params['announcementGroup'], { emitEvent: false });
    }

    this.updateDisplayedBuildingTypes();
    this.updateAvailableAreaSqMeters();
    this.updateAvailableAccomodationPrices();
    this.announcementAccomodationSearchForm.markAsDirty();
  }


  onBuildingTypeCheckboxChanged(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = +input.value;
    console.log(value);
    if (input.checked) {
      this.selectedBuildingTypes.push(new FormControl<number>(value, { nonNullable: true }));
    } else {
      const index = this.selectedBuildingTypes.controls.findIndex(c => c.value === value);
      if (index !== -1) this.selectedBuildingTypes.removeAt(index);
    }
  }

  updateDisplayedBuildingTypes() {
    this.displayedBuildingTypes = [];
    let ids = this.selectedBuildingTypes.value;
    this.displayedBuildingTypes.push(...this.buildingTypes.filter(c => ids.includes(c.id)));
    this.displayedBuildingTypes.push(...this.buildingTypes.filter(c => !ids.includes(c.id)));
  }

  updateAvailableAreaSqMeters() {
    var formValue = this.announcementAccomodationSearchForm.value;
    if (formValue.areaSqMetersUpper == null) {
      this.currentlyAvailableAreqSqMetersLower = [...areaSqMetersLower];
    }
    else {
      this.currentlyAvailableAreqSqMetersLower = areaSqMetersLower.filter(a => a.value < formValue.areaSqMetersUpper);
    }

    if (formValue.areaSqMetersLower == null) {
      this.currentlyAvailableAreqSqMetersUpper = [...areaSqMetersUpper];
    }
    else {
      this.currentlyAvailableAreqSqMetersUpper = areaSqMetersUpper.filter(a => a.value > formValue.areaSqMetersLower);
    }
  }

  updateAvailableAccomodationPrices() {
    var formValue = this.announcementAccomodationSearchForm.value;
    if (formValue.priceUpper == null) {
      this.currenltyAvailableAccomodationPricesLower = [...accomodationPriceOptionsLower];
    }
    else {
      this.currenltyAvailableAccomodationPricesLower = accomodationPriceOptionsLower.filter(a => a.value < formValue.priceUpper);
    }

    if (formValue.priceLower == null) {
      this.currenltyAvailableAccomodationPricesUpper = [...accomodationPriceOptionsUpper];
    }
    else {
      this.currenltyAvailableAccomodationPricesUpper = accomodationPriceOptionsUpper.filter(a => a.value > formValue.priceLower);
    }
  }
}
