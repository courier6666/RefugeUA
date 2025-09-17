import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from '../admin/admin.component';
import { UserService } from '../../core/services/user/user-service';
import { SearchBarComponent } from '../../shared/components/search-bar/search-bar.component';
import { UsersAdminPreviewComponent } from '../../shared/components/users-admin-preview/users-admin-preview.component';
import { UsersManagementComponent } from './users-management/users-management.component';
import { PagingNavigationComponent } from '../../shared/paging-navigation/paging-navigation.component';
import { AnnouncementsModerationComponent } from './announcements-moderation/announcements-moderation.component';
import { AnnouncementModerationService } from '../../core/services/announcements/announcements-moderation-service';
import { AnnouncementModerationPreviewComponent } from '../../shared/components/announcement-moderation-preview/announcement-moderation-preview.component';

@NgModule({
  declarations: [
    AdminComponent,
    UsersManagementComponent,
    AnnouncementsModerationComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SearchBarComponent,
    PagingNavigationComponent,
    UsersAdminPreviewComponent,
    AnnouncementModerationPreviewComponent
  ],
  providers: [
    UserService,
    AnnouncementModerationService
  ],
})
export class AdminModule { }
