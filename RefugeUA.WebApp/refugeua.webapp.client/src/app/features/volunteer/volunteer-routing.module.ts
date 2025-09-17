import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VolunteerEventsComponent } from './volunteer-events/volunteer-events.component';
import { VolunteerGroupsComponent } from './volunteer-groups/volunteer-groups.component';
import { VolunteerEventDetailComponent } from './volunteer-events/detail/volunteer-event-detail.component';
import { VolunteerEventCreateEditComponent } from './volunteer-events/create-edit/volunteer-event-create-edit.component';
import { VolunteerGroupCreateEditComponent } from './volunteer-groups/create-edit/volunteer-group-create-edit.component';
import { VolunteerGroupDetailComponent } from './volunteer-groups/detail/volunteer-group-detail.component';
import { VolunteerGroupParticipantsComponent } from './volunteer-groups/volunteer-group-participants/volunteer-group-participants.component';
import { VolunteerGroupAdministratorsComponent } from './volunteer-groups/volunteer-group-administrators/volunteer-group-administrators.component';
import { VolunteerEventParticipantsComponent } from './volunteer-events/volunteer-event-participants/volunteer-event-participants.component';

const routes: Routes = [
  { path: 'events', component: VolunteerEventsComponent },
  { path: 'groups', component: VolunteerGroupsComponent },
  { path: 'events/:id/detail', component: VolunteerEventDetailComponent },
  { path: 'events/create', component: VolunteerEventCreateEditComponent },
  { path: 'events/:id/edit', component: VolunteerEventCreateEditComponent },
  { path: 'groups/create', component: VolunteerGroupCreateEditComponent },
  { path: 'groups/:id/edit', component: VolunteerGroupCreateEditComponent },
  { path: 'groups/:id/detail', component: VolunteerGroupDetailComponent },
  { path: 'groups/:id/followers', component: VolunteerGroupParticipantsComponent },
  { path: 'groups/:id/admins', component: VolunteerGroupAdministratorsComponent },
  { path: 'events/:id/participants', component: VolunteerEventParticipantsComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VolunteerRoutingModule { }
