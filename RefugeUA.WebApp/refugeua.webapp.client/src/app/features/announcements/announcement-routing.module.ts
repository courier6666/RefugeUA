import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AnnouncementWorkComponent } from './work/announcement-work.component';
import { AnnouncementEducationComponent } from './education/announcement-education.component';
import { AnnouncementAccomodationComponent } from './accomodation/announcement-accomodation.component';
import { AccomodationAnnouncementDetailComponent } from './accomodation/detail/accomodation-announcement-detail.component';
import { EducationAnnouncementDetailComponent } from './education/detail/education-announcement-detail.component';
import { WorkAnnouncementDetailComponent } from './work/detail/work-announcement-detail.component';
import { AnnouncementResponsesComponent } from './responses/announcement-responses.component';
import { AnnouncementWorkCreateEditComponentComponent } from './work/create-edit/announcement-work-create-edit-component.component';
import { AnnouncementEducationCreateEditComponent } from './education/create-edit/announcement-education-create-edit.component';
import { AnnouncementAccomodationCreateEditComponent } from './accomodation/create-edit/announcement-accomodation-create-edit.component';
import { AnnouncementGroupsComponent } from './groups/announcement-groups.component';

const routes: Routes = [
  {
    path: 'work',
    component: AnnouncementWorkComponent
  },
  {
    path: 'education',
    component: AnnouncementEducationComponent
  },
  {
    path: 'accomodation',
    component: AnnouncementAccomodationComponent
  },
  {
    path: 'accomodation/:id/detail',
    component: AccomodationAnnouncementDetailComponent
  },
  {
    path: 'education/:id/detail',
    component: EducationAnnouncementDetailComponent
  },
  {
    path: 'work/:id/detail',
    component: WorkAnnouncementDetailComponent
  },
  {
    path: ':id/responses',
    component: AnnouncementResponsesComponent
  },
  {
    path: 'work/create',
    component: AnnouncementWorkCreateEditComponentComponent
  },
  {
    path: 'education/create',
    component: AnnouncementEducationCreateEditComponent
  },
  {
    path: 'accomodation/create',
    component: AnnouncementAccomodationCreateEditComponent
  },
  {
    path: 'work/:id/edit',
    component: AnnouncementWorkCreateEditComponentComponent
  },
  {
    path: 'education/:id/edit',
    component: AnnouncementEducationCreateEditComponent
  },
  {
    path: 'accomodation/:id/edit',
    component: AnnouncementAccomodationCreateEditComponent
  },
  {
    path: 'work/:id/groups',
    component: AnnouncementGroupsComponent
  },
  {
    path: 'education/:id/groups',
    component: AnnouncementGroupsComponent
  },
  {
    path: 'accomodation/:id/groups',
    component: AnnouncementGroupsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AnnouncementRoutingModule { }
