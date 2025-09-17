import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, ValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';
import { baseAnnouncementForm } from '../../../../shared/util/build-base-forms';
import { AnnouncementWorkService } from '../../../../core/services/announcements/work/announcement-work-service';
import { WorkAnnouncement, WorkCategory } from '../../../../core/models';
import { CreateWorkAnnouncementCommand } from '../../../../core/api-models/create-work-announcement-command';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
  selector: 'app-announcement-work-create-edit-component',
  standalone: false,
  templateUrl: './announcement-work-create-edit-component.component.html',
  styleUrl: './announcement-work-create-edit-component.component.css'
})
export class AnnouncementWorkCreateEditComponentComponent implements OnInit {
  workAnnouncementForm: FormGroup = new FormGroup([]);
  categories: WorkCategory[] = []
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  currentRoute: string = "create";
  id: number = 0;
  isAnnouncementLoaded = false;
  isLoadingFailed = false;
  submitErrorResult: string[] = [];

  constructor(private workAnnouncementService: AnnouncementWorkService, private router: Router, private route: ActivatedRoute, private location: Location) {
    this.workAnnouncementForm = new FormGroup({});

  }

  ngOnInit(): void {
    this.currentRoute = this.route.snapshot.url.at(-1)?.path!;

    this.workAnnouncementService.getCategories().subscribe({
      next: (data) => this.categories = data,
      error: (err) => console.log(err)
    });

    let formBuild = new FormBuilder();
    this.workAnnouncementForm = formBuild.group({
      ...baseAnnouncementForm().controls,
      jobPosition: new FormControl('', [Validators.required, Validators.maxLength(200)]),
      companyName: new FormControl('', [Validators.required, Validators.maxLength(400)]),
      salaryLower: new FormControl<number | null>(null, [Validators.min(8000), Validators.max(500000), this.salaryLowerMustBeLessThanUpper()]),
      salaryUpper: new FormControl<number | null>(null, [Validators.min(8000), Validators.max(500000), this.salaryUpperMustBeGreaterThanLower()]),
      requirementsContent: new FormControl('', [Validators.required, Validators.maxLength(1024)]),
      workCategory: new FormControl<number | null>(null, Validators.required)
    });

    this.salaryLower?.valueChanges.subscribe((res) => {
      this.salaryUpper?.updateValueAndValidity({ emitEvent: false });
    });

    this.salaryUpper?.valueChanges.subscribe((res) => {
      this.salaryLower?.updateValueAndValidity({ emitEvent: false });
    });

    if (this.currentRoute == "edit") {
      this.id = +this.route.snapshot.paramMap.get('id')!;
      this.workAnnouncementService.getWorkAnnouncement(this.id).subscribe({
        next: (response) => {
          this.isAnnouncementLoaded = true;

          this.title?.setValue(response.title);
          this.content?.setValue(response.content);
          this.jobPosition?.setValue(response.jobPosition);
          this.companyName?.setValue(response.companyName);
          this.salaryLower?.setValue(response.salaryLower);
          this.salaryUpper?.setValue(response.salaryUpper);
          this.requirementsContent?.setValue(response.requirementsContent);
          this.workCategory?.setValue(response.workCategoryId);

          this.phoneNumber?.setValue(response.contactInformation?.phoneNumber);
          this.email?.setValue(response.contactInformation?.email);
          this.telegram?.setValue(response.contactInformation?.telegram);
          this.viber?.setValue(response.contactInformation?.viber);
          this.facebook?.setValue(response.contactInformation?.facebook);

          this.country?.setValue(response.address?.country);
          this.region?.setValue(response.address?.region);
          this.district?.setValue(response.address?.district);
          this.settlement?.setValue(response.address?.settlement);
          this.street?.setValue(response.address?.street);
          this.postalCode?.setValue(response.address?.postalCode);
        },
        error: (err) => {
          this.isAnnouncementLoaded = false;
          this.isLoadingFailed = true;
        }
      });
    }

    this.workAnnouncementForm.valueChanges.subscribe({
      next: () => {
        this.isFormSubmitted = false;
        this.submitErrorResult = [];
      }
    });
  }

  salaryLowerMustBeLessThanUpper(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

      let lower = +control.value;
      if (!lower) {
        return null;
      }

      if (!this.salaryUpper?.value) {
        return null;
      }

      let upper = +this.salaryUpper?.value;
      if (lower > upper) {
        return { 'lowerGreaterUpper': 'Lower salary cannot be greater than upper salary!' } as ValidationErrors;
      }

      return null;
    };
  }

  salaryUpperMustBeGreaterThanLower(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {

      let upper = +control.value;
      if (!upper) {
        return null;
      }

      if (!this.salaryLower?.value) {
        return null;
      }

      let lower = +this.salaryLower?.value;
      if (lower > upper) {
        return { 'upperLessLower': 'Upper salary cannot be less than lower salary!' } as ValidationErrors;
      }

      return null;
    };
  }

  get title() {
    return this.workAnnouncementForm.get('title');
  }

  get content() {
    return this.workAnnouncementForm.get('content');
  }

  get phoneNumber() {
    return this.workAnnouncementForm.get('contactInformation.phoneNumber');
  }

  get email() {
    return this.workAnnouncementForm.get('contactInformation.email');
  }

  get telegram() {
    return this.workAnnouncementForm.get('contactInformation.telegram');
  }

  get viber() {
    return this.workAnnouncementForm.get('contactInformation.viber');
  }

  get facebook() {
    return this.workAnnouncementForm.get('contactInformation.facebook');
  }

  get country() {
    return this.workAnnouncementForm.get('address.country');
  }

  get region() {
    return this.workAnnouncementForm.get('address.region');
  }

  get district() {
    return this.workAnnouncementForm.get('address.district');
  }

  get settlement() {
    return this.workAnnouncementForm.get('address.settlement');
  }

  get street() {
    return this.workAnnouncementForm.get('address.street');
  }

  get postalCode() {
    return this.workAnnouncementForm.get('address.postalCode');
  }

  get jobPosition() {
    return this.workAnnouncementForm.get('jobPosition');
  }

  get companyName() {
    return this.workAnnouncementForm.get('companyName');
  }

  get salaryLower() {
    return this.workAnnouncementForm.get('salaryLower');
  }

  get salaryUpper() {
    return this.workAnnouncementForm.get('salaryUpper');
  }

  get requirementsContent() {
    return this.workAnnouncementForm.get('requirementsContent');
  }

  get workCategory() {
    return this.workAnnouncementForm.get('workCategory');
  }

  createCommand(): CreateWorkAnnouncementCommand {
    let formValues = this.workAnnouncementForm.value;
    let workAnnouncement: CreateWorkAnnouncementCommand = {
      title: formValues.title,
      content: formValues.content,
      jobPosition: formValues.jobPosition,
      companyName: formValues.companyName,
      salaryLower: (formValues.salaryLower != null) ? formValues.salaryLower : undefined,
      salaryUpper: (formValues.salaryUpper != null) ? formValues.salaryUpper : undefined,
      requirementsContent: formValues.requirementsContent,
      workCategoryId: formValues.workCategory,
      contactInformation: {
        phoneNumber: formValues.contactInformation.phoneNumber,
        email: (formValues.contactInformation.email != '') ? formValues.contactInformation.email : undefined,
        telegram: (formValues.contactInformation.telegram != '') ? formValues.contactInformation.telegram : undefined,
        viber: (formValues.contactInformation.viber != '') ? formValues.contactInformation.viber : undefined,
        facebook: (formValues.contactInformation.facebook != '') ? formValues.contactInformation.facebook : undefined
      },
      address: {
        country: formValues.address.country,
        region: formValues.address.region,
        district: formValues.address.district,
        settlement: formValues.address.settlement,
        street: formValues.address.street,
        postalCode: formValues.address.postalCode
      }
    }

    return workAnnouncement;
  }

  onSubmitEdit() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let workAnnouncement = this.createCommand();

    this.workAnnouncementService.editWorkAnnouncement(this.id, workAnnouncement).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/announcements/work', this.id, 'detail']);
      },
      error: (err) => {
        this.resultSuccess = false;
        this.submitErrorResult = err.message.split('\n');
      }
    });
  }

  onSumbitCreate() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let workAnnouncement = this.createCommand();

    this.workAnnouncementService.createWorkAnnouncement(workAnnouncement).subscribe({
      next: (result) => {
        this.router.navigate(['/announcements/work', result, 'detail']);
        this.resultSuccess = true;
      },
      error: (err) => {
        this.resultSuccess = false;
        this.submitErrorResult = err.message.split('\n');
      }
    });
  }

  onSubmit() {
    if (this.currentRoute == "edit") {
      this.onSubmitEdit();
    }
    else {
      this.onSumbitCreate();
    }
  }

  onCancel() {
    this.location.back();
  }
}
