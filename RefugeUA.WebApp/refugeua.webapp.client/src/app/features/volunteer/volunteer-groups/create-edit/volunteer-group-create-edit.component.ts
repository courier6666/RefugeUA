import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, Form } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { VolunteerGroupService } from '../../../../core/services/volunteer/volunteer-group-services';
import { CreateVolunteerGroupCommand } from '../../../../core/api-models/create-volunteer-group-command';
import { Location } from '@angular/common';

@Component({
  selector: 'app-volunteer-group-create-edit',
  standalone: false,
  templateUrl: './volunteer-group-create-edit.component.html',
  styleUrl: './volunteer-group-create-edit.component.css'
})
export class VolunteerGroupCreateEditComponent implements OnInit {
  volunteerGroupForm: FormGroup = new FormGroup([]);

  includeAddress = false;
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  currentRoute: string = "create";
  id: number = 0;
  isGroupLoaded = false;
  isLoadingFailed = false;
  submitErrorResult: string[] = [];

  constructor(private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private volunteerGroupService: VolunteerGroupService) {

  }

  get title() {
    return this.volunteerGroupForm.get('title');
  }

  get descriptionContent() {
    return this.volunteerGroupForm.get('descriptionContent');
  }

  ngOnInit(): void {
    this.currentRoute = this.route.snapshot.url.at(-1)?.path!;

    this.volunteerGroupForm = this.fb.group({
      title: new FormControl('', [Validators.required, Validators.maxLength(200)]),
      descriptionContent: new FormControl('', [Validators.required, Validators.maxLength(4096)]),
    });

    if (this.currentRoute == 'edit') {
      this.id = +this.route.snapshot.paramMap.get('id')!;
      this.volunteerGroupService.getVolunteerGroupById(this.id).subscribe({
        next: (res) => {

          this.isGroupLoaded = true;
          this.title?.setValue(res.title);
          this.descriptionContent?.setValue(res.descriptionContent);
        },
        error: (err) => {
          this.isGroupLoaded = false;
          this.isLoadingFailed = true;
        }
      });
    }

    this.volunteerGroupForm.valueChanges.subscribe({
      next: () => this.isFormSubmitted = false
    });
  }

  createCommand(): CreateVolunteerGroupCommand {
    let formValues = this.volunteerGroupForm.value;

    console.log(formValues);

    let createGroupCommand: CreateVolunteerGroupCommand = {
      title: formValues.title,
      descriptionContent: formValues.descriptionContent
    }

    console.log(createGroupCommand);
    return createGroupCommand;
  }

  onSubmitEdit() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let groupCommand = this.createCommand();

    this.volunteerGroupService.editVolunteerGroup(this.id, groupCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/volunteer/groups', this.id, 'detail']);
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

    let groupCommand = this.createCommand();

    this.volunteerGroupService.createVolunteerGroup(groupCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/volunteer/groups', result, 'detail']);
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
