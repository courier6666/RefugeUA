import { CommonModule } from '@angular/common';
import { AnnouncementRoutingModule } from './announcement-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AnnouncementComponent } from './announcement.component';
import { RouterModule } from '@angular/router';
import { AnnouncementWorkComponent } from './work/announcement-work.component';
import { AnnouncementAccomodationComponent } from './accomodation/announcement-accomodation.component';
import { AnnouncementEducationComponent } from './education/announcement-education.component';
import { FormGroup, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { AnnouncementWorkPreviewComponent } from './work/announcement-work-preview/announcement-work-preview.component';
import { PagingNavigationComponent } from '../../shared/paging-navigation/paging-navigation.component';
import { AnnouncementEducationPreviewComponent } from './education/announcement-education-preview/announcement-education-preview.component';
import { AnnouncementAccomodationPreviewComponent } from './accomodation/announcement-accomodation-preview/announcement-accomodation-preview.component';
import { AddressComponent } from '../../shared/components/address/address.component';
import { SearchBarComponent } from "../../shared/components/search-bar/search-bar.component";
import { AccomodationAnnouncementDetailComponent } from './accomodation/detail/accomodation-announcement-detail.component';
import { EducationAnnouncementDetailComponent } from './education/detail/education-announcement-detail.component';
import { WorkAnnouncementDetailComponent } from './work/detail/work-announcement-detail.component';
import { AnnouncementResponsesComponent } from './responses/announcement-responses.component';
import { ContentComponent } from '../../shared/components/content/content.component';
import { RequirementsContentComponent } from './work/requirements-content/requirements-content.component';
import { ResponseCreateComponent } from './responses/create/response-create.component';
import { ContactInformationComponent } from '../../shared/components/contact-information/contact-information.component';
import { AnnouncementWorkCreateEditComponentComponent } from './work/create-edit/announcement-work-create-edit-component.component';
import { AnnouncementEducationCreateEditComponent } from './education/create-edit/announcement-education-create-edit.component';
import { AnnouncementAccomodationCreateEditComponent } from './accomodation/create-edit/announcement-accomodation-create-edit.component';
import { MultipleFileUploadComponent } from '../../shared/components/multiple-file-upload/multiple-file-upload.component';
import { AddressDetailComponent } from '../../shared/components/address-detail/address-detail.component';
import { AnnouncementWorkService } from '../../core/services/announcements/work/announcement-work-service';
import { AnnouncementEducationService } from '../../core/services/announcements/education/announcement-education-service';
import { AnnouncementAccomodationService } from '../../core/services/announcements/accomodation/announcement-accomodation-service';
import { AnnouncementAcceptenceReasonInputComponent } from '../../shared/components/announcement-acceptence-reason-input/announcement-acceptence-reason-input.component';
import { AnnouncementDetailAcceptenceComponent } from './base-announcement/detail/announcement-detail-acceptence/announcement-detail-acceptence.component';
import { AnnouncementService } from '../../core/services/announcements/announcement-service';
import { AnnouncementGroupsComponent } from './groups/announcement-groups.component';

@NgModule({
  declarations: [
    AnnouncementComponent,
    AnnouncementWorkComponent,
    AnnouncementAccomodationComponent,
    AnnouncementEducationComponent,
    AnnouncementWorkPreviewComponent,
    AnnouncementEducationPreviewComponent,
    AnnouncementAccomodationPreviewComponent,
    AccomodationAnnouncementDetailComponent,
    EducationAnnouncementDetailComponent,
    WorkAnnouncementDetailComponent,
    AnnouncementResponsesComponent,
    RequirementsContentComponent,
    ResponseCreateComponent,
    AnnouncementWorkCreateEditComponentComponent,
    AnnouncementEducationCreateEditComponent,
    AnnouncementAccomodationCreateEditComponent,
    AnnouncementDetailAcceptenceComponent,
    AnnouncementGroupsComponent
  ],
  imports: [
    CommonModule,
    AnnouncementRoutingModule,
    RouterModule,
    ReactiveFormsModule,
    PagingNavigationComponent,
    AddressComponent,
    SearchBarComponent,
    ContentComponent,
    ContactInformationComponent,
    MultipleFileUploadComponent,
    AddressDetailComponent,
    AnnouncementAcceptenceReasonInputComponent
],
  providers: [
    AnnouncementWorkService,
    AnnouncementEducationService,
    AnnouncementAccomodationService,
    AnnouncementService
  ]
})
export class AnnouncementModule { }
