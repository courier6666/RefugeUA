import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, Form } from '@angular/forms';
import { MentalSupportArticleService } from '../../../../core/services/mental-support/mental-support-article-service';
import { Router, ActivatedRoute } from '@angular/router';
import { CreateMentalSupportArticleCommand } from '../../../../core/api-models/create-mental-support-article-command';
import { Location } from '@angular/common';

@Component({
  selector: 'app-mental-suppport-article-create-edit',
  standalone: false,
  templateUrl: './mental-suppport-article-create-edit.component.html',
  styleUrl: './mental-suppport-article-create-edit.component.css'
})
export class MentalSuppportArticleCreateEditComponent {
  articleForm: FormGroup = new FormGroup([]);
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
    private articleService: MentalSupportArticleService) {

  }
  ngOnInit(): void {
    this.currentRoute = this.route.snapshot.url.at(-1)?.path!;

    let formBuild = new FormBuilder();
    this.articleForm = formBuild.group({
      title: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(200)]),
      content: new FormControl<string | null>(null, [Validators.required, Validators.maxLength(8192)]),
    });

    if (this.currentRoute == 'edit') {
      this.id = +this.route.snapshot.paramMap.get('id')!;
      this.articleService.getArticleById(this.id).subscribe({
        next: (res) => {
          this.title?.setValue(res.title);
          this.content?.setValue(res.content);

          this.isLoaded = true;
          this.isLoadingFailed = false;

        },
        error: (err) => {
          this.isLoaded = true;
          this.isLoadingFailed = true;
        }
      });
    }

    this.articleForm.valueChanges.subscribe({
      next: () => this.isFormSubmitted = false
    });
  }

  get title() {
    return this.articleForm.get('title');
  }

  get content() {
    return this.articleForm.get('content');
  }

  createCommand(): CreateMentalSupportArticleCommand {
    let formValues = this.articleForm.value;

    let command: CreateMentalSupportArticleCommand = {
      title: formValues.title,
      content: formValues.content,
    };

    return command;
  }


  onSubmitEdit() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let command = this.createCommand();

    this.articleService.editArticle(this.id, command).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/mental-support/articles', this.id, 'detail']);
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

    this.articleService.createArticle(command).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/mental-support/articles', result, 'detail']);
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
