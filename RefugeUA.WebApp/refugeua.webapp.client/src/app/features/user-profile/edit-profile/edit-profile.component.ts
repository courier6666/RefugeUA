import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UserRole } from '../../../core/enums/user-role';
import { FormGroup, FormControl, AbstractControl } from '@angular/forms';
import { Validators, ValidatorFn, ValidationErrors } from '@angular/forms';
import { AuthService } from '../../../core/services/auth/auth-service';
import { EditProfileCommand } from '../../../core/api-models/edit-profile-command';
import { Location } from '@angular/common';
import { UserService } from '../../../core/services/user/user-service';
import { Roles } from '../../../core/constants/user-roles-constants';
import { FileService } from '../../../core/services/util/file-service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-edit-profile',
  standalone: false,
  templateUrl: './edit-profile.component.html',
  styleUrl: './edit-profile.component.css'
})
export class EditProfileComponent implements OnInit {
  UserRole = UserRole;
  Roles = Roles;
  editProfileForm: FormGroup = new FormGroup({});
  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  profileLoadedStatus?: boolean;
  profileImageFile: File | null = null;
  profileImageSrc: string | ArrayBuffer | null = null;
  pictureInputElement!: HTMLElement | null;
  userId?: number = undefined;
  @ViewChild('picture') pictureElement!: ElementRef<HTMLInputElement>;

  constructor(public authService: AuthService,
    private userService: UserService,
    private location: Location,
    private fileService: FileService,
    private route: ActivatedRoute,
    private router: Router) {

  }

  get maxDateForBirthday(): Date {
    let nowDate = new Date();
    nowDate.setFullYear(nowDate.getFullYear() - 16);
    return nowDate;
  }

  onImageRemove() {
    this.profileImageFile = null;
    this.profileImageSrc = null;
  }

  ngOnInit(): void {
    this.userId = +this.route.snapshot.params['id'];
    this.editProfileForm = new FormGroup({
      firstName: new FormControl<string>('', [
        Validators.required,
        Validators.maxLength(100)
      ]),
      lastName: new FormControl<string>('', [
        Validators.required,
        Validators.maxLength(100)
      ]),
      dateOfBirth: new FormControl<Date | null>(null, [
        Validators.required,
        this.dateInPastValidator()
      ]),
      phoneNumber: new FormControl<string>('', {
        validators: [
          Validators.required,
          Validators.pattern(/^\+?[1-9]\d{1,14}$/)
        ],
      }),
      district: new FormControl<string | null>('',),
      profilePicture: new FormControl<File | null>(null)
    });

    this.editProfileForm.valueChanges.subscribe({
      next: (result) => {
        console.log(result);
        this.isFormSubmitted = false;
        let formValues = this.editProfileForm.value;
        this.district?.clearValidators();
        if (this.authService.userClaims?.role == Roles.CommunityAdmin) {
          this.district?.addValidators([Validators.required, Validators.maxLength(100)]);
        }
        else {
          this.district?.addValidators([Validators.maxLength(100)]);
        }

        console.log(result);


        this.district?.updateValueAndValidity({ emitEvent: false });
      }
    });

    let getUserObservable: Observable<any>;

    if(!this.userId)
    {
      getUserObservable = this.userService.getMyProfile();
    }
    else {
      getUserObservable = this.userService.getUserProfile(this.userId);
    }

    getUserObservable.subscribe({
      next: (res) => {
        this.firstName?.setValue(res.firstName, { emitEvent: false });
        this.lastName?.setValue(res.lastName, { emitEvent: false });
        this.dateOfBirth?.setValue(res.dateOfBirth.toString().split('T')[0], { emitEvent: false });
        this.phoneNumber?.setValue(res.phoneNumber, { emitEvent: false });
        this.district?.setValue(res.district, { emitEvent: false });

        if (res.profileImageUrl) {

          this.fileService.getImage(res.profileImageUrl).subscribe({
            next: (res1) => {
              this.profileImageFile = res1;
              const reader = new FileReader();
              reader.onload = () => {
                this.profileImageSrc = reader.result;
              };

              reader.readAsDataURL(this.profileImageFile!);
              this.profileLoadedStatus = true;
            },
            error: (err) => {
              this.profileLoadedStatus = false;
              console.log(err);
            }
          });
        }
        else {
          this.profileLoadedStatus = true;
        }
      },
      error: (err) => {
        this.profileLoadedStatus = false;
        console.log(err);
      }
    });
  }

  validateImage(imageFile: File): Promise<any> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      let errors: ValidationErrors = {};

      if (!imageFile) {
        errors['fileMissing'] = 'No file selected';
        this.profilePicture?.setErrors(errors);
        return resolve(errors);
      }

      if (!imageFile.name.toLowerCase().endsWith('.jpg') &&
        !imageFile.name.toLowerCase().endsWith('.jpeg')) {
        errors['wrongFormat'] = 'Profile picture must be in .jpg or .jpeg format only!';
      }

      reader.onload = () => {
        const img = new Image();

        img.onload = () => {

          if (imageFile!.size / 1024 / 1024 > 2) {
            errors['imageSizeExceed'] = 'Profile image size must not exceed 2 MB';
          }

          if (img.width < 200 || img.height < 200) {
            errors['imageResolution'] = 'Profile image must be at least 200x200 pixels.';
          }

          let aspectRatio = img.width / img.height;
          if (aspectRatio < 0.75 || aspectRatio > 16 / 9) {
            errors['aspectRatioOutOfRange'] = 'Aspect ratio must be between 3:4 and 16:9';
          }

          if (Object.keys(errors).length > 0) {
            return resolve(errors);
          } else {
            return resolve(null);
          }
        };

        img.onerror = () => {
          errors['wrongFormat'] = 'Profile picture must be in .jpg or .jpeg format only!';
          return resolve(errors);
        };

        img.src = reader.result as string;
      };

      reader.onerror = () => {
        errors['errorLoading'] = 'Error loading profile picture.';
        return resolve(errors);
      };

      reader.readAsDataURL(imageFile!);
    });
  }

  async onFileSelected(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      let errors = await this.validateImage(event.target.files[0]);
      console.log(errors);
      this.profilePicture?.setErrors(errors);
      this.profilePicture?.markAsTouched();
      if (!errors || Object.keys(errors).length == 0) {
        this.profileImageFile = event.target.files[0];

        const reader = new FileReader();
        reader.onload = () => {
          this.profileImageSrc = reader.result;
        };

        reader.readAsDataURL(this.profileImageFile!);

      }
    }
  }

  dateInPastValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const date = control.value;
      return date && new Date(date) >= new Date()
        ? { dateNotInPast: true }
        : null;
    };
  }

  get firstName() { return this.editProfileForm.get('firstName'); }
  get lastName() { return this.editProfileForm.get('lastName'); }
  get phoneNumber() { return this.editProfileForm.get('phoneNumber'); }
  get district() { return this.editProfileForm.get('district'); }
  get dateOfBirth() { return this.editProfileForm.get('dateOfBirth'); }
  get profilePicture() { return this.editProfileForm.get('profilePicture'); }

  onCancel() {
    this.location.back();
  }

  onSubmit() {
    this.resultSuccess = null;
    this.isFormSubmitted = true;
    let editProfileCommand: EditProfileCommand = {
      firstName: this.firstName?.value,
      lastName: this.lastName?.value,
      dateOfBirth: this.dateOfBirth?.value,
      phoneNumber: this.phoneNumber?.value,
      district: this.district?.value,
      profilePicture: this.profileImageFile
    };

    if (!this.userId) {
      this.userService.editMyProfile(editProfileCommand).subscribe({
        next: (res) => {
          this.resultSuccess = true;
          this.router.navigate(['/profile']);
        },
        error: (err) => {
          this.resultSuccess = false;
        }
      });
    }
    else {
      this.userService.editUserProfile(this.userId, editProfileCommand).subscribe({
        next: (res) => {
          this.resultSuccess = true;
          this.router.navigate(['/user', this.userId, 'profile']);
        },
        error: (err) => {
          this.resultSuccess = false;
        }
      });
    }
  }
}
