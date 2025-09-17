import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './features/pages/home/home.component';
import { AboutComponent } from './features/pages/about/about.component';
import { AnnouncementComponent } from './features/announcements/announcement.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { VolunteerComponent } from './features/volunteer/volunteer.component';
import { MentalSupportComponent } from './features/mental-support/mental-support.component';
import { ConfirmEmailComponent } from './features/auth/confirm-email/confirm-email.component';
import { SendEmailConfirmationComponent } from './features/auth/send-email-confirmation/send-email-confirmation.component';
import { AdminComponent } from './features/admin/admin.component';
import { MyResponsesComponent } from './features/user-profile/my-responses/my-responses.component';
import { MyProfileComponent } from './features/user-profile/my-profile/my-profile.component';
import { MyAnnouncementsComponent } from './features/user-profile/my-announcements/my-announcements.component';
import { EditProfileComponent } from './features/user-profile/edit-profile/edit-profile.component';
import { ViewProfileComponent } from './features/user-profile/view-profile/view-profile.component';
import { MyParticipatedVolunteerEventsComponent } from './features/user-profile/my-participated-volunteer-events/my-participated-volunteer-events.component';
import { MyVolunteerEventsComponent } from './features/user-profile/my-volunteer-events/my-volunteer-events.component';
import { MyVolunteerGroupsComponent } from './features/user-profile/my-volunteer-groups/my-volunteer-groups.component';
import { MyFollowedVolunteerGroupsComponent } from './features/user-profile/my-followed-volunteer-groups/my-followed-volunteer-groups.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'about', component: AboutComponent},
  {
    path: 'announcements',
    loadChildren: () =>
      import('./features/announcements/announcement.module').then(m => m.AnnouncementModule),
    component: AnnouncementComponent
  },
  {
    path: 'volunteer',
    loadChildren: () =>
      import('./features/volunteer/volunteer.module').then(m => m.VolunteerModule),
    component: VolunteerComponent
  },
  {
    path: 'mental-support',
        loadChildren: () =>
      import('./features/mental-support/mental-support.module').then(m => m.MentalSupportModule),
    component: MentalSupportComponent
  },
  {
    path: 'admin',
        loadChildren: () =>
      import('./features/admin/admin.module').then(m => m.AdminModule),
    component: AdminComponent
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'confirm-email', component: ConfirmEmailComponent },
  { path: 'send-email-confirmation', component: SendEmailConfirmationComponent },
  { path: 'announcements/responses/mine', component: MyResponsesComponent },
  { path: 'profile', component: MyProfileComponent },
  { path: 'profile/edit', component: EditProfileComponent },
  { path: 'user/:id/profile/edit', component: EditProfileComponent },
  { path: 'announcements/mine', component: MyAnnouncementsComponent },
  { path: 'user/:id/profile', component: ViewProfileComponent },
  { path: 'volunteer/events/participated', component: MyParticipatedVolunteerEventsComponent },
  { path: 'volunteer/events/mine', component: MyVolunteerEventsComponent },
  { path: 'volunteer/groups/mine', component: MyVolunteerGroupsComponent },
  { path: 'volunteer/groups/followed', component: MyFollowedVolunteerGroupsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
