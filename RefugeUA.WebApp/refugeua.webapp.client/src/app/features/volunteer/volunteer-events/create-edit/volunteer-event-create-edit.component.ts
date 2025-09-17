import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, Form, ReactiveFormsModule } from '@angular/forms';
import { baseAddressFormNotRequired, baseAddressFormRequired } from '../../../../shared/util/build-base-forms';
import { VolunteerEventType } from '../../../../core/enums/volunteer-event-type-enum';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { CreateVolunteerEventCommand } from '../../../../core/api-models/create-volunteer-event-command';
import { CreateVolunteerEventScheduleItemCommand } from '../../../../core/api-models/create-volunteer-schedule-item-command';
import { VolunteerEventService } from '../../../../core/services/volunteer/volunteer-event-service';
import { CreateAddressCommand } from '../../../../core/api-models/create-address-command';
import { VolunteerGroupService } from '../../../../core/services/volunteer/volunteer-group-services';
import { VolunteerGroup } from '../../../../core/models';
import { scheduleItemMustBetweenDates } from '../../../../shared/validators/schedule-must-between-dates-validator';

@Component({
  selector: 'app-volunteer-event-create-edit',
  standalone: false,
  templateUrl: './volunteer-event-create-edit.component.html',
  styleUrl: './volunteer-event-create-edit.component.css'
})
export class VolunteerEventCreateEditComponent implements OnInit {
  VolunteerEventType = VolunteerEventType;
  availableVolunteerGroups: VolunteerGroup[] = [];
  volunteerEventForm: FormGroup = new FormGroup([]);
  includeAddress = false;
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  currentRoute: string = "create";
  id: number = 0;
  isEventLoaded = false;
  isLoadingFailed = false;
  submitErrorResult: string[] = [];

  constructor(private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private volunteerEventService: VolunteerEventService,
    private volunteerGroupService: VolunteerGroupService) {

  }

  ngOnInit(): void {
    this.currentRoute = this.route.snapshot.url.at(-1)?.path!;

    this.volunteerEventForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      content: ['', [Validators.required, Validators.maxLength(4096)]],
      volunteerEventType: new FormControl<VolunteerEventType | null>(null, Validators.required),
      startTime: new FormControl<Date | null>(null, Validators.required),
      endTime: new FormControl<Date | null>(null, Validators.required),
      scheduleItems: new FormArray([]),
      onlineConferenceLink: new FormControl<string>('', Validators.maxLength(500)),
      donationLink: new FormControl<string>('', Validators.maxLength(500)),
      volunteerGroupId: new FormControl<number | null>(null)
    });

    this.startTime?.valueChanges.subscribe({
      next: (res) => {
        this.scheduleItems.controls.forEach(el => {
          el.get('time')?.updateValueAndValidity();
          console.log(el);
        })
      }
    });

    this.endTime?.valueChanges.subscribe({
      next: (res) => {
        this.scheduleItems.controls.forEach(el => {
          el.get('time')?.updateValueAndValidity();
          console.log(el);
        })
      }
    });

    this.volunteerGroupService.getAvailableVolunteerGroupPreviews().subscribe({
      next: (res) => {
        this.availableVolunteerGroups = res;


        if (this.currentRoute == 'edit') {
          this.id = +this.route.snapshot.paramMap.get('id')!;
          this.volunteerEventService.getVolunteerEvent(this.id).subscribe({
            next: (res) => {
              console.log(res);
              this.isEventLoaded = true;
              this.title?.setValue(res.title);
              this.content?.setValue(res.content);
              this.volunteerEventType.setValue(res.volunteerEventType);
              this.startTime?.setValue(res.startTime.toString().split("T")[0]);
              this.endTime?.setValue(res.endTime.toString().split("T")[0]);
              this.onlineConferenceLink?.setValue(res.onlineConferenceLink);
              this.donationLink?.setValue(res.donationLink);
              if (res.volunteerGroupId) {
                this.volunteerGroupId?.setValue(res.volunteerGroupId);
              }

              this.includeAddress = res.address != null;
              if (res.address) {
                this.volunteerEventForm.addControl('address', baseAddressFormNotRequired());
                this.country?.setValue(res.address.country);
                this.region?.setValue(res.address.region);
                this.district?.setValue(res.address.district);
                this.settlement?.setValue(res.address.settlement);
                this.street?.setValue(res.address.street);
                this.postalCode?.setValue(res.address.postalCode);
              }

              if (res.scheduleItems && res.scheduleItems.length > 0) {
                for (let item of res.scheduleItems) {
                  this.scheduleItems.push(this.fb.group({
                    id: item.id,
                    time: new FormControl<Date | null>(item.startTime, [Validators.required, scheduleItemMustBetweenDates(this.startTime!, this.endTime!)]),
                    description: new FormControl<string | null>(item.description, [Validators.required, Validators.maxLength(400)]),
                  }));
                }
              }
            },
            error: (err) => {
              this.isEventLoaded = true;
              this.isLoadingFailed = true;
            }
          });
        }

        this.volunteerEventForm.valueChanges.subscribe({
          next: (res) => {
            this.isFormSubmitted = false;
          }
        });
      },
      error: (err) => {
        console.log(err);
        this.isEventLoaded = false;
        this.isLoadingFailed = true;
      }
    });
  }

  get title() {
    return this.volunteerEventForm.get('title');
  }

  get content() {
    return this.volunteerEventForm.get('content');
  }

  get volunteerEventType(): FormControl<VolunteerEventType | null> {
    return this.volunteerEventForm.get('volunteerEventType') as FormControl<VolunteerEventType | null>;
  }

  get startTime() {
    return this.volunteerEventForm.get('startTime');
  }

  get endTime() {
    return this.volunteerEventForm.get('endTime');
  }

  get scheduleItems(): FormArray {
    return this.volunteerEventForm.get('scheduleItems') as FormArray;
  }

  get onlineConferenceLink() {
    return this.volunteerEventForm.get('onlineConferenceLink');
  }

  get donationLink() {
    return this.volunteerEventForm.get('donationLink');
  }

  get volunteerGroupId() {
    return this.volunteerEventForm.get('volunteerGroupId');
  }

  get country() {
    return this.volunteerEventForm.get('address.country');
  }

  get region() {
    return this.volunteerEventForm.get('address.region');
  }

  get district() {
    return this.volunteerEventForm.get('address.district');
  }

  get settlement() {
    return this.volunteerEventForm.get('address.settlement');
  }

  get street() {
    return this.volunteerEventForm.get('address.street');
  }

  get postalCode() {
    return this.volunteerEventForm.get('address.postalCode');
  }

  onIncludeAddressChecked(event: Event) {
    const input = event.target as HTMLInputElement;
    console.log(input.checked);
    if (input.checked) {
      this.volunteerEventForm.addControl('address', baseAddressFormRequired());
      this.includeAddress = true;
    } else {
      this.volunteerEventForm.removeControl('address');
      this.includeAddress = false;
    }

  }
  addScheduleItem() {
    let formBuild = new FormBuilder();
    this.scheduleItems.push(formBuild.group({
      id: 0,
      time: new FormControl<Date | null>(null, [Validators.required, scheduleItemMustBetweenDates(this.startTime!, this.endTime!)]),
      description: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(400)]),
    }));
  }

  removeScheduleItem(index: number) {
    this.scheduleItems.removeAt(index);
  }

  createCommand(): CreateVolunteerEventCommand {
    let formValues = this.volunteerEventForm.value;

    let address: CreateAddressCommand | undefined = undefined;

    console.log(formValues);

    if (formValues.address) {
      address = {
        country: formValues.address.country,
        region: formValues.address.region,
        district: formValues.address.district,
        settlement: formValues.address.settlement,
        street: formValues.address.street,
        postalCode: formValues.address.postalCode
      };
    }

    let createEventCommand: CreateVolunteerEventCommand = {
      title: formValues.title,
      content: formValues.content,
      eventType: formValues.volunteerEventType,
      startTime: formValues.startTime,
      endTime: formValues.endTime,
      onlineConferenceLink: formValues.onlineConferenceLink,
      donationLink: formValues.donationLink,
      volunteerGroupId: formValues.volunteerGroupId,
      address: address,
      scheduleItems: formValues.scheduleItems.map((val: any): CreateVolunteerEventScheduleItemCommand => {
        return {
          id: val.id,
          startTime: val.time,
          description: val.description
        };
      })
    }

    console.log(createEventCommand);
    return createEventCommand;
  }

  onSubmitEdit() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let eventCommand = this.createCommand();

    this.volunteerEventService.editVolunteerEvent(this.id, eventCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/volunteer/events', this.id, 'detail']);
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

    let eventCommand = this.createCommand();

    this.volunteerEventService.createVolunteerEvent(eventCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/volunteer/events', result, 'detail']);
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
