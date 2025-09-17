import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VolunteerComponent } from './volunteer.component';
import { VolunteerEventsComponent } from './volunteer-events/volunteer-events.component';
import { VolunteerRoutingModule } from './volunteer-routing.module';
import { VolunteerGroupsComponent } from './volunteer-groups/volunteer-groups.component';
import { VolunteerEventPreviewComponent } from './volunteer-events/volunteer-event-preview/volunteer-event-preview.component';
import { SearchBarComponent } from '../../shared/components/search-bar/search-bar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PagingNavigationComponent } from '../../shared/paging-navigation/paging-navigation.component';
import { VolunteerEventTypeToStringPipe } from '../../shared/pipes/volunteer-event-type-to-string.pipe';
import { VolunteerGroupPreviewComponent } from './volunteer-groups/volunteer-group-preview/volunteer-group-preview.component';
import { VolunteerEventDetailComponent } from './volunteer-events/detail/volunteer-event-detail.component';
import { ContentComponent } from '../../shared/components/content/content.component';
import { VolunteerEventCreateEditComponent } from './volunteer-events/create-edit/volunteer-event-create-edit.component';
import { AddressDetailComponent } from '../../shared/components/address-detail/address-detail.component';
import { VolunteerGroupCreateEditComponent } from './volunteer-groups/create-edit/volunteer-group-create-edit.component';
import { VolunteerGroupDetailComponent } from './volunteer-groups/detail/volunteer-group-detail.component';
import { AddressComponent } from '../../shared/components/address/address.component';
import { VolunteerGroupParticipantsComponent } from './volunteer-groups/volunteer-group-participants/volunteer-group-participants.component';
import { UserPreviewComponent } from '../../shared/components/user-preview/user-preview.component';
import { VolunteerGroupAdministratorsComponent } from './volunteer-groups/volunteer-group-administrators/volunteer-group-administrators.component';
import { VolunteerEventParticipantsComponent } from './volunteer-events/volunteer-event-participants/volunteer-event-participants.component';

@NgModule({
  declarations: [
    VolunteerComponent,
    VolunteerEventsComponent,
    VolunteerGroupsComponent,
    VolunteerEventPreviewComponent,
    VolunteerGroupPreviewComponent,
    VolunteerEventDetailComponent,
    VolunteerEventCreateEditComponent,
    VolunteerGroupCreateEditComponent,
    VolunteerGroupDetailComponent,
    VolunteerGroupParticipantsComponent,
    VolunteerGroupAdministratorsComponent,
    VolunteerEventParticipantsComponent
  ],
  imports: [
    CommonModule,
    VolunteerRoutingModule,
    SearchBarComponent,
    ReactiveFormsModule,
    FormsModule,
    PagingNavigationComponent,
    VolunteerEventTypeToStringPipe,
    ContentComponent,
    AddressDetailComponent,
    AddressComponent,
    UserPreviewComponent,
  ],
})
export class VolunteerModule { }
