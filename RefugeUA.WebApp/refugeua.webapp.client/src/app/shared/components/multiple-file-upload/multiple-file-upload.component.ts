import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormGroup, FormBuilder, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-multiple-file-upload',
  standalone: true,
  templateUrl: './multiple-file-upload.component.html',
  styleUrl: './multiple-file-upload.component.css',
  imports: [ReactiveFormsModule, CommonModule]
})
export class MultipleFileUploadComponent {
  uploadForm: FormGroup;
  selectedFiles: File[] = [];

  constructor(private fb: FormBuilder) {
    this.uploadForm = this.fb.group({});
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files) {
      this.selectedFiles = Array.from(input.files);
    }
  }

  onSubmit(): void {
    if (this.selectedFiles.length) {
      const formData = new FormData();
      this.selectedFiles.forEach((file, index) =>
        formData.append('files[]', file, file.name)
      );

    }
  }
}
