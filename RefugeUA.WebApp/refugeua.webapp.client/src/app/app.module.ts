import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { registerLocaleData } from '@angular/common';
import localeUk from '@angular/common/locales/uk';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationMenuComponent } from './shared/components/navigation-menu/navigation-menu.component';
import { HomeComponent } from './features/pages/home/home.component';
import { AboutComponent } from './features/pages/about/about.component';
import { LoginComponent } from './features/auth/login/login.component';
import { RegisterComponent } from './features/auth/register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthPanelComponent } from './shared/components/auth-panel/auth-panel.component';
import { VolunteerModule } from './features/volunteer/volunteer.module';
import { SearchBarComponent } from './shared/components/search-bar/search-bar.component';
import { VolunteerEventTypeToStringPipe } from './shared/pipes/volunteer-event-type-to-string.pipe';
import { ContactInformationComponent } from './shared/components/contact-information/contact-information.component';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './core/services/auth/auth-service';
import { SendEmailConfirmationComponent } from './features/auth/send-email-confirmation/send-email-confirmation.component';
import { ConfirmEmailComponent } from './features/auth/confirm-email/confirm-email.component';
import { SessionStorageService } from './core/services/util/session-storage-service';
import { AuthTokenInterceptor } from './core/interceptors/auth-token-interceptor.interceptor';
import { UnauthorizedInterceptor } from './core/interceptors/unauthorized.interceptor';
import { InvernalServerErrorComponent } from './features/pages/invernal-server-error/invernal-server-error.component';
import { BaseUserNavPanelComponent } from './shared/components/user-nav-panels/base-user-nav-panel/base-user-nav-panel.component';
import { AdminNavPanelComponent } from './shared/components/user-nav-panels/admin-nav-panel/admin-nav-panel.component';
import { MilitaryOrFamilyNavPanelComponent } from './shared/components/user-nav-panels/military-or-family-nav-panel/military-or-family-nav-panel.component';
import { LocalCitizenNavPanelComponent } from './shared/components/user-nav-panels/local-citizen-nav-panel/local-citizen-nav-panel.component';
import { VolunteerNavPanelComponent } from './shared/components/user-nav-panels/volunteer-nav-panel/volunteer-nav-panel.component';
import { CommunityAdminNavPanelComponent } from './shared/components/user-nav-panels/community-admin-nav-panel/community-admin-nav-panel.component';
import { RouterModule } from '@angular/router';
import { AnnouncementAcceptenceReasonInputComponent } from './shared/components/announcement-acceptence-reason-input/announcement-acceptence-reason-input.component';
import { MyResponsesComponent } from './features/user-profile/my-responses/my-responses.component';
import { PagingNavigationComponent } from './shared/paging-navigation/paging-navigation.component';
import { MyProfileComponent } from './features/user-profile/my-profile/my-profile.component';
import { ProfileComponent } from './features/user-profile/profile/profile.component';
import { ViewProfileComponent } from './features/user-profile/view-profile/view-profile.component';
import { EditProfileComponent } from './features/user-profile/edit-profile/edit-profile.component';
import { MyAnnouncementsComponent } from './features/user-profile/my-announcements/my-announcements.component';
import { MyAnnouncementPreviewComponent } from './features/user-profile/my-announcements/my-announcement-preview/my-announcement-preview.component';
import { AddressComponent } from './shared/components/address/address.component';
import { MyVolunteerEventsComponent } from './features/user-profile/my-volunteer-events/my-volunteer-events.component';
import { MyParticipatedVolunteerEventsComponent } from './features/user-profile/my-participated-volunteer-events/my-participated-volunteer-events.component';
import { ProfileVolunteerEventPreviewComponent } from './features/user-profile/profile-volunteer-event-preview/profile-volunteer-event-preview.component';
import { UserPreviewComponent } from './shared/components/user-preview/user-preview.component';
import { ProfileVolunteerGroupPreviewComponent } from './features/user-profile/profile-volunteer-group-preview/profile-volunteer-group-preview.component';
import { MyVolunteerGroupsComponent } from './features/user-profile/my-volunteer-groups/my-volunteer-groups.component';
import { MyFollowedVolunteerGroupsComponent } from './features/user-profile/my-followed-volunteer-groups/my-followed-volunteer-groups.component';
import { FileService } from './core/services/util/file-service';

registerLocaleData(localeUk);

@NgModule({
  declarations: [
    AppComponent,
    NavigationMenuComponent,
    HomeComponent,
    AboutComponent,
    RegisterComponent,
    LoginComponent,
    AuthPanelComponent,
    SendEmailConfirmationComponent,
    ConfirmEmailComponent,
    InvernalServerErrorComponent,
    BaseUserNavPanelComponent,
    AdminNavPanelComponent,
    MilitaryOrFamilyNavPanelComponent,
    LocalCitizenNavPanelComponent,
    VolunteerNavPanelComponent,
    CommunityAdminNavPanelComponent,
    MyResponsesComponent,
    MyProfileComponent,
    ProfileComponent,
    ViewProfileComponent,
    EditProfileComponent,
    MyAnnouncementsComponent,
    MyAnnouncementPreviewComponent,
    MyVolunteerEventsComponent,
    MyParticipatedVolunteerEventsComponent,
    ProfileVolunteerEventPreviewComponent,
    ProfileVolunteerGroupPreviewComponent,
    MyVolunteerGroupsComponent,
    MyFollowedVolunteerGroupsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    VolunteerModule,
    SearchBarComponent,
    VolunteerEventTypeToStringPipe,
    ContactInformationComponent,
    PagingNavigationComponent,
    AddressComponent,
    PagingNavigationComponent
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'uk'},
    CookieService,
    { provide: AuthService},
    { provide: SessionStorageService },
    { provide: FileService },
    { provide: HTTP_INTERCEPTORS, useClass: AuthTokenInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: UnauthorizedInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
