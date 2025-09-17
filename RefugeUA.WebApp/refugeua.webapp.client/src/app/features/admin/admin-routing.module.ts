import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersManagementComponent } from './users-management/users-management.component';
import { AnnouncementsModerationComponent } from './announcements-moderation/announcements-moderation.component';

const routes: Routes = [
  {
    path: 'users-management',
    component: UsersManagementComponent
  },
  {
    path: 'announcements-moderation',
    component: AnnouncementsModerationComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
