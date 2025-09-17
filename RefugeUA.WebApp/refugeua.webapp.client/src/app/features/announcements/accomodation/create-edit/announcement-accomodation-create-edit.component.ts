import { Component, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, FormArray, Form } from '@angular/forms';
import { baseAnnouncementForm } from '../../../../shared/util/build-base-forms';
import { ValidationErrors } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BuildingType, buildingTypes } from '../../../../core/constants/building-type';
import { AnnouncementAccomodationService } from '../../../../core/services/announcements/accomodation/announcement-accomodation-service';
import { Location } from '@angular/common';
import { CreateAccomodationAnnouncementCommand } from '../../../../core/api-models/create-accomodation-announcement';
import { FileService } from '../../../../core/services/util/file-service';
import type { Image } from '../../../../core/models';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-announcement-accomodation-create-edit',
  standalone: false,
  templateUrl: './announcement-accomodation-create-edit.component.html',
  styleUrl: './announcement-accomodation-create-edit.component.css'
})
export class AnnouncementAccomodationCreateEditComponent {
  accomodationAnnouncementForm: FormGroup = new FormGroup([]);
  buildingTypes: BuildingType[] = [...buildingTypes];

  isFormSubmitted: boolean = false;
  resultSuccess: boolean | null = null;
  currentRoute: string = "create";
  id: number = 0;
  isAnnouncementLoaded = false;
  isLoadingFailed = false;
  isTargetGroupTouched = false;

  submitErrorResult: string[] = [];

  imageFiles: File[] = [];
  imageSrcs: (string | ArrayBuffer)[] = [];
  imageInputElement!: HTMLElement | null;
  @ViewChild('image') imageElement!: ElementRef<HTMLInputElement>;

  constructor(private accomodationAnnouncementService: AnnouncementAccomodationService,
    private router: Router,
    private route: ActivatedRoute,
    private location: Location,
    private fileService: FileService) {
  }


  ngOnInit(): void {
    this.currentRoute = this.route.snapshot.url.at(-1)?.path!;

    let formBuild = new FormBuilder();

    this.accomodationAnnouncementForm = formBuild.group({
      ...baseAnnouncementForm().controls,
      buildingType: new FormControl<string | null>(null, Validators.required),
      petsAllowed: new FormControl<boolean>(false),
      floors: new FormControl<number | null>(null, [Validators.required, Validators.min(1), Validators.max(60)]),
      numberOfRooms: new FormControl<number | null>(null, [Validators.required, Validators.min(1)]),
      capacity: new FormControl<number | null>(null, [Validators.required, Validators.min(1)]),
      areaSqMeters: new FormControl<number | null>(null, [Validators.min(5), Validators.max(1000)]),
      isFree: new FormControl(true),
      price: new FormControl<number | null>({ value: null, disabled: true }, [Validators.min(1)]),
      image: new FormControl<File | null>(null)
    });

    this.accomodationAnnouncementForm.get('isFree')!.valueChanges.subscribe((isFree: boolean) => {
      const feeControl = this.accomodationAnnouncementForm.get('price');
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
      this.accomodationAnnouncementService.getAccomodationAnnouncement(this.id).subscribe({
        next: (response) => {

          console.log(response);
          this.isAnnouncementLoaded = true;

          this.title?.setValue(response.title);
          this.content?.setValue(response.content);
          this.buildingType?.setValue(response.buildingType);
          this.petsAllowed?.setValue(response.petsAllowed);
          this.floors?.setValue(response.floors);
          this.numberOfRooms?.setValue(response.numberOfRooms);
          this.capacity?.setValue(response.capacity);
          this.areaSqMeters?.setValue(response.areaSqMeters);
          this.isFree?.setValue(response.isFree);
          this.price?.setValue(response.price);

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

          if (response.images && response.images.length > 0) {
            this.loadImagesForForm(response.images).then();
          }

        },
        error: (err) => {
          this.isAnnouncementLoaded = false;
          this.isLoadingFailed = true;
        }
      });
    }

    this.accomodationAnnouncementForm.valueChanges.subscribe({
      next: () => this.isFormSubmitted = false
    });
  }

  async loadImagesForForm(images: Image[]) {
    try {
      for (let img of images) {
        const fileRes = await firstValueFrom(this.fileService.getImage(img.path));
        this.imageFiles.push(fileRes);

        const dataUrl = await this.readFileAsDataURL(fileRes);
        if (dataUrl) {
          this.imageSrcs.push(dataUrl);
        }
        else {
          throw new Error("Failed to load an image.");
        }
      }

      this.isAnnouncementLoaded = true;
    } catch (err) {
      this.isAnnouncementLoaded = false;
      console.log(err);
    }
  }

  readFileAsDataURL(file: File): Promise<string | ArrayBuffer | null> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
      reader.readAsDataURL(file);
    });
  }

  onImageRemove(id: number) {
    this.imageFiles.splice(id, 1);
    this.imageSrcs.splice(id, 1);
  }

  validateImage(imageFile: File): Promise<any> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      let errors: ValidationErrors = {};

      if (this.imageFiles.length >= 6) {
        errors['tooManyPhotos'] = 'Only six photos are allowed.';
        return resolve(errors);
      }

      if (!imageFile) {
        errors['fileMissing'] = 'No file selected';
        this.image?.setErrors(errors);
        return resolve(errors);
      }

      if (!imageFile.name.toLowerCase().endsWith('.jpg') &&
        !imageFile.name.toLowerCase().endsWith('.jpeg')) {
        errors['wrongFormat'] = 'Picture must be in .jpg or .jpeg format only!';
      }

      reader.onload = () => {
        const img = new Image();

        img.onload = () => {

          if (imageFile!.size / 1024 / 1024 > 2) {
            errors['imageSizeExceed'] = 'Image size must not exceed 2 MB';
          }

          if (img.width < 200 || img.height < 200) {
            errors['imageResolution'] = 'Image must be at least 800x600 pixels.';
          }

          let aspectRatio = img.width / img.height;
          if (aspectRatio < 0.75 || aspectRatio > 16 / 9) {
            errors['aspectRatioOutOfRange'] = 'Aspect ratio must be between 4:3 and 16:9';
          }

          if (Object.keys(errors).length > 0) {
            return resolve(errors);
          } else {
            return resolve(null);
          }
        };

        img.onerror = () => {
          errors['wrongFormat'] = 'Picture must be in .jpg or .jpeg format only!';
          return resolve(errors);
        };

        img.src = reader.result as string;
      };

      reader.onerror = () => {
        errors['errorLoading'] = 'Error loading picture.';
        return resolve(errors);
      };

      reader.readAsDataURL(imageFile!);
    });
  }

  async onFileSelected(event: any) {
    if (event.target.files && event.target.files.length > 0) {
      let file = event.target.files[0];
      this.image?.reset();
      let errors = await this.validateImage(file);
      console.log(errors);
      this.image?.setErrors(errors);
      this.image?.markAsTouched();
      if (!errors || Object.keys(errors).length == 0) {
        this.imageFiles.push(file);

        const reader = new FileReader();
        reader.onload = () => {
          this.imageSrcs.push(reader.result!);
        };

        reader.readAsDataURL(file);
      }
    }

    console.log(event.target.files);
  }

  get title() {
    return this.accomodationAnnouncementForm.get('title');
  }

  get content() {
    return this.accomodationAnnouncementForm.get('content');
  }

  get phoneNumber() {
    return this.accomodationAnnouncementForm.get('contactInformation.phoneNumber');
  }

  get email() {
    return this.accomodationAnnouncementForm.get('contactInformation.email');
  }

  get telegram() {
    return this.accomodationAnnouncementForm.get('contactInformation.telegram');
  }

  get viber() {
    return this.accomodationAnnouncementForm.get('contactInformation.viber');
  }

  get facebook() {
    return this.accomodationAnnouncementForm.get('contactInformation.facebook');
  }

  get country() {
    return this.accomodationAnnouncementForm.get('address.country');
  }

  get region() {
    return this.accomodationAnnouncementForm.get('address.region');
  }

  get district() {
    return this.accomodationAnnouncementForm.get('address.district');
  }

  get settlement() {
    return this.accomodationAnnouncementForm.get('address.settlement');
  }

  get street() {
    return this.accomodationAnnouncementForm.get('address.street');
  }

  get postalCode() {
    return this.accomodationAnnouncementForm.get('address.postalCode');
  }

  get buildingType() {
    return this.accomodationAnnouncementForm.get('buildingType');
  }

  get petsAllowed() {
    return this.accomodationAnnouncementForm.get('petsAllowed');
  }

  get floors() {
    return this.accomodationAnnouncementForm.get('floors');
  }

  get numberOfRooms() {
    return this.accomodationAnnouncementForm.get('numberOfRooms');
  }

  get capacity() {
    return this.accomodationAnnouncementForm.get('capacity');
  }

  get areaSqMeters() {
    return this.accomodationAnnouncementForm.get('areaSqMeters');
  }

  get isFree() {
    return this.accomodationAnnouncementForm.get('isFree');
  }

  get price() {
    return this.accomodationAnnouncementForm.get('price');
  }

  get image() { return this.accomodationAnnouncementForm.get('image'); }

  createCommand(): CreateAccomodationAnnouncementCommand {
    let formValues = this.accomodationAnnouncementForm.value;
    let accomodationAnnouncement: CreateAccomodationAnnouncementCommand = {
      title: formValues.title,
      content: formValues.content,
      buildingType: formValues.buildingType,
      petsAllowed: formValues.petsAllowed,
      numberOfRooms: formValues.numberOfRooms,
      floors: formValues.floors,
      areaSqMeters: formValues.areaSqMeters,
      price: formValues.isFree ? null : formValues.price,
      capacity: formValues.capacity,
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
      },
      images: this.imageFiles
    };
    console.log(accomodationAnnouncement);
    return accomodationAnnouncement;
  }

  onSubmitEdit() {
    this.isFormSubmitted = true;
    this.resultSuccess = null;

    let accomodationAnnouncementCommand = this.createCommand();

    this.accomodationAnnouncementService.editAccomodationAnnouncement(this.id, accomodationAnnouncementCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/announcements/accomodation', this.id, 'detail']);
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

    let accomodationAnnouncementCommand = this.createCommand();

    this.accomodationAnnouncementService.createAccomodationAnnouncement(accomodationAnnouncementCommand).subscribe({
      next: (result) => {
        this.resultSuccess = true;
        this.router.navigate(['/announcements/accomodation', result, 'detail']);
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
