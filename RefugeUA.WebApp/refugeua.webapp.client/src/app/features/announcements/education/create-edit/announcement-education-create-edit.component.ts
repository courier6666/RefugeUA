import { Component } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, Form } from '@angular/forms';
import { baseAnnouncementForm } from '../../../../shared/util/build-base-forms';
import { TargetGroup } from '../../../../core/frontend-only-models/target-group';
import { educationTargetGroups } from '../../../../core/constants/target-groups';
import { Router, ActivatedRoute } from '@angular/router';
import { AnnouncementEducationService } from '../../../../core/services/announcements/education/announcement-education-service';
import { EducationType } from '../../../../core/frontend-only-models/education-type';
import { educationTypes } from '../../../../core/constants/education-types';
import { CreateEducationAnnouncementCommand } from '../../../../core/api-models/create-education-announcement-command';
import { TimeSpan } from '../../../../shared/util/time-span';
import { Location } from '@angular/common';

@Component({
  selector: 'app-announcement-education-create-edit',
  standalone: false,
  templateUrl: './announcement-education-create-edit.component.html',
  styleUrl: './announcement-education-create-edit.component.css'
})
export class AnnouncementEducationCreateEditComponent {
  educationAnnouncementForm: FormGroup = new FormGroup([]);
  targetGroups: TargetGroup[] = [];
  educationTypesValues: EducationType[] = [];
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  currentRoute: string = "create";
  id: number = 0;
  isAnnouncementLoaded = false;
  isLoadingFailed = false;
  isTargetGroupTouched = false;
  submitErrorResult: string[] = [];

  constructor(private educationAnnouncementService: AnnouncementEducationService, private router: Router, private route: ActivatedRoute, private location: Location) {
    this.educationAnnouncementForm = new FormGroup({});

  }

  ngOnInit(): void {
    this.isTargetGroupTouched = false;
    this.currentRoute = this.route.snapshot.url.at(-1)?.path!;

    let formBuild = new FormBuilder();
    this.targetGroups = [...educationTargetGroups];
    this.educationTypesValues = [...educationTypes];

    this.educationAnnouncementForm = formBuild.group({
      ...baseAnnouncementForm().controls,
      educationType: new FormControl<number | null>(null, Validators.required),
      institutionName: new FormControl('', [Validators.required, Validators.maxLength(200)]),
      targetGroups: new FormArray<FormControl<number>>([]),
      language: new FormControl('Українська', [Validators.required]),
      isFree: new FormControl(true),
      fee: new FormControl({ value: null, disabled: true }, [Validators.min(0)]),
      duration: new FormControl<number | null>(null, [Validators.required, Validators.max(2191)]),
    });

    this.educationAnnouncementForm.get('isFree')!.valueChanges.subscribe((isFree: boolean) => {
      const feeControl = this.educationAnnouncementForm.get('fee');
      if (!isFree) {
        feeControl!.enable();
        feeControl!.setValidators([Validators.required, Validators.min(0)]);
      } else {
        feeControl!.disable();
        feeControl!.clearValidators();
      }
      feeControl!.updateValueAndValidity();
    });

    if (this.currentRoute == "edit") {
      this.id = +this.route.snapshot.paramMap.get('id')!;
      this.educationAnnouncementService.getEducationAnnouncement(this.id).subscribe({
        next: (response) => {

          this.isAnnouncementLoaded = true;

          this.title?.setValue(response.title);
          this.content?.setValue(response.content);
          this.educationType?.setValue(this.educationTypesValues.find(e => e.name == response.educationType)?.id);
          this.institutionName?.setValue(response.institutionName);
          this.duration?.setValue(response.duration);
          this.language?.setValue(response.language);
          this.isFree?.setValue(response.fee == null);
          this.fee?.setValue(response.fee);
          this.selectedTargetGroups.clear();

          let targetGroupsInResponse = response.targetGroups;
          this.targetGroups.filter(t => targetGroupsInResponse.includes(t.name)).map(t => t.id).forEach((id) => this.selectedTargetGroups.push(new FormControl<number>(id, { nonNullable: true })));

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

    this.educationAnnouncementForm.valueChanges.subscribe({
      next: () => this.isFormSubmitted = false
    });
  }

  get title() {
    return this.educationAnnouncementForm.get('title');
  }

  get content() {
    return this.educationAnnouncementForm.get('content');
  }

  get institutionName() {
    return this.educationAnnouncementForm.get('institutionName');
  }

  get educationType() {
    return this.educationAnnouncementForm.get('educationType');
  }

  get duration() {
    return this.educationAnnouncementForm.get('duration');
  }

  get language() {
    return this.educationAnnouncementForm.get('language');
  }

  get isFree() {
    return this.educationAnnouncementForm.get('isFree');
  }

  get fee() {
    return this.educationAnnouncementForm.get('fee');
  }

  get contactInformation() {
    return this.educationAnnouncementForm.get('contactInformation');
  }

  get phoneNumber() {
    return this.contactInformation?.get('phoneNumber');
  }

  get email() {
    return this.contactInformation?.get('email');
  }

  get telegram() {
    return this.contactInformation?.get('telegram');
  }

  get viber() {
    return this.contactInformation?.get('viber');
  }

  get facebook() {
    return this.contactInformation?.get('facebook');
  }

  get address() {
    return this.educationAnnouncementForm.get('address');
  }

  get country() {
    return this.address?.get('country');
  }

  get region() {
    return this.address?.get('region');
  }

  get district() {
    return this.address?.get('district');
  }

  get settlement() {
    return this.address?.get('settlement');
  }

  get street() {
    return this.address?.get('street');
  }

  get postalCode() {
    return this.address?.get('postalCode');
  }


  get selectedTargetGroups(): FormArray<FormControl<number>> {
    return this.educationAnnouncementForm.get('targetGroups') as FormArray<FormControl<number>>;
  }

  isTargetGroupSelected(id: number): boolean {
    return this.selectedTargetGroups.value.includes(id);
  }

  onTargetGroupCheckboxChanged(event: Event) {
    const input = event.target as HTMLInputElement;
    const value = +input.value;

    if (input.checked) {
      this.selectedTargetGroups.push(new FormControl<number>(value, { nonNullable: true }));
    } else {
      const index = this.selectedTargetGroups.controls.findIndex(c => c.value === value);
      if (index !== -1) this.selectedTargetGroups.removeAt(index);
    }

    this.isTargetGroupTouched = true;
  }

  getEducationTypeName(id: number): string {
    return this.educationTypesValues.find(e => e.id == id)!.name
  }

  getTargetGroupsName(): string[] {
    return this.targetGroups.filter(t => this.selectedTargetGroups.value.includes(t.id)).map(t => t.name);
  }

  createCommand(): CreateEducationAnnouncementCommand {
    let formValues = this.educationAnnouncementForm.value;
    let educationAnnouncement: CreateEducationAnnouncementCommand = {
      title: formValues.title,
      content: formValues.content,
      educationType: this.getEducationTypeName(+formValues.educationType),
      targetGroups: this.getTargetGroupsName(),
      institutionName: formValues.institutionName,
      fee: (formValues.isFree) ? null : formValues.fee,
      language: formValues.language,
      duration: +formValues.duration,
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
    };

    return educationAnnouncement;
  }

  onSubmitEdit() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let educationAnnouncementCommand = this.createCommand();

    this.educationAnnouncementService.editEducationAnnouncement(this.id, educationAnnouncementCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/announcements/education', this.id, 'detail']);
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

    let educationAnnouncementCommand = this.createCommand();

    this.educationAnnouncementService.createEducationAnnouncement(educationAnnouncementCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/announcements/education', result, 'detail']);
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
