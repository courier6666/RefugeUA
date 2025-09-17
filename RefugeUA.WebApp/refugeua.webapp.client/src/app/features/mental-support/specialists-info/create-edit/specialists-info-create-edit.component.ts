import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, Form } from '@angular/forms';
import { baseContactInfoFormRequired } from '../../../../shared/util/build-base-forms';
import { Router, ActivatedRoute } from '@angular/router';
import { PsychologistInformationService } from '../../../../core/services/mental-support/psychologist-information-service';
import { Location } from '@angular/common';
import { CreatePsychologistInfoCommand } from '../../../../core/api-models/create-psychologist-info-command';

@Component({
  selector: 'app-specialists-info-create-edit',
  standalone: false,
  templateUrl: './specialists-info-create-edit.component.html',
  styleUrl: './specialists-info-create-edit.component.css'
})
export class SpecialistsInfoCreateEditComponent {
  specialistsInfoForm: FormGroup = new FormGroup([]);
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  currentRoute: string = "create";
  id: number = 0;
  isLoaded = false;
  isLoadingFailed = false;
  submitErrorResult: string[] = [];

  constructor(private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private psychologistInfoService: PsychologistInformationService) {

  }

  ngOnInit(): void {
    this.currentRoute = this.route.snapshot.url.at(-1)?.path!;

    let formBuild = new FormBuilder();
    this.specialistsInfoForm = formBuild.group({
      title: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(200)]),
      content: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(1024)]),
      contactInformation: baseContactInfoFormRequired()
    });

    if (this.currentRoute == 'edit') {
      this.id = +this.route.snapshot.paramMap.get('id')!;
      this.psychologistInfoService.getPsychologistInformationById(this.id).subscribe({
        next: (res) => {
          this.title?.setValue(res.title);
          this.content?.setValue(res.description);

          this.phoneNumber?.setValue(res.contact?.phoneNumber);
          this.email?.setValue(res.contact?.email);
          this.telegram?.setValue(res.contact?.telegram);
          this.viber?.setValue(res.contact?.viber);
          this.facebook?.setValue(res.contact?.facebook);
          this.isLoaded = true;
          this.isLoadingFailed = false;

        },
        error: (err) => {
          this.isLoaded = true;
          this.isLoadingFailed = true;
        }
      });
    }

    this.specialistsInfoForm.valueChanges.subscribe({
      next: () => this.isFormSubmitted = false
    });
  }

  get title() {
    return this.specialistsInfoForm.get('title');
  }

  get content() {
    return this.specialistsInfoForm.get('content');
  }

  get phoneNumber() {
    return this.specialistsInfoForm.get('contactInformation.phoneNumber');
  }

  get email() {
    return this.specialistsInfoForm.get('contactInformation.email');
  }

  get telegram() {
    return this.specialistsInfoForm.get('contactInformation.telegram');
  }

  get viber() {
    return this.specialistsInfoForm.get('contactInformation.viber');
  }

  get facebook() {
    return this.specialistsInfoForm.get('contactInformation.facebook');
  }

  createCommand(): CreatePsychologistInfoCommand {
    let formValues = this.specialistsInfoForm.value;

    let command: CreatePsychologistInfoCommand = {
      title: formValues.title,
      description: formValues.content,
      contactInformation: {
        phoneNumber: formValues.contactInformation.phoneNumber,
        email: formValues.contactInformation.email,
        telegram: formValues.contactInformation.telegram,
        viber: formValues.contactInformation.viber,
        facebook: formValues.contactInformation.facebook
      }
    };

    return command;
  }


  onSubmitEdit() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let command = this.createCommand();

    this.psychologistInfoService.editPsychologistInformation(this.id, command).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/mental-support/specialists-infos', this.id, 'detail']);
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

    let command = this.createCommand();

    this.psychologistInfoService.createPsychologistInformation(command).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/mental-support/specialists-infos', result, 'detail']);
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
