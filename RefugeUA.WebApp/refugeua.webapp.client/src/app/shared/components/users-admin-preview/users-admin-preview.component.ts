import { Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../../core/models';
import { OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user/user-service';
import { DatePipe } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-users-admin-preview',
  standalone: true,
  templateUrl: './users-admin-preview.component.html',
  styleUrl: './users-admin-preview.component.css',
  imports: [DatePipe, RouterModule]
})
export class UsersAdminPreviewComponent implements OnInit {
  @Input({ required: true }) user!: User;

  acceptUserStateSuccess?: boolean;
  isAcceptLoading: boolean = false;
  confirmEmailUserStateSuccess?: boolean;
  isConfirmEmailLoading: boolean = false;
  deleteUserStateSuccess?: boolean;
  isDeleteLoading: boolean = false;

  constructor(private router: Router, private route: ActivatedRoute, private userService: UserService) {

  }

  ngOnInit(): void {

  }

  onAcceptUser() {
    this.acceptUserStateSuccess = undefined;
    this.isAcceptLoading = true;
    this.userService.acceptUser(this.user.id!).subscribe({
      next: (response) => {
        this.acceptUserStateSuccess = true;
        this.user.isAccepted = true;
        this.isAcceptLoading = false;
      },
      error: (error) => {
        this.acceptUserStateSuccess = false;
        this.isAcceptLoading = false;
        console.error("Error accepting user:", error);
      }
    });
  }

  onForbidUser() {
    this.acceptUserStateSuccess = undefined;
    this.isAcceptLoading = true;
    this.userService.forbidUser(this.user.id!).subscribe({
      next: (response) => {
        this.acceptUserStateSuccess = true;
        this.user.isAccepted = false
        this.isAcceptLoading = false;
      },
      error: (error) => {
        this.acceptUserStateSuccess = false;

        console.error("Error accepting user:", error);
      }
    });
  }

  onConfirmEmailUser() {
    this.confirmEmailUserStateSuccess = undefined;
    this.isConfirmEmailLoading = true;
    this.userService.confirmEmailUser(this.user.id!).subscribe({
      next: (response) => {
        this.confirmEmailUserStateSuccess = true;
        this.user.isEmailConfirmed = true;
        this.isConfirmEmailLoading = false;
      },
      error: (error) => {
        this.confirmEmailUserStateSuccess = false;
        this.isConfirmEmailLoading = false;
        console.error("Error accepting user:", error);
      }
    });
  }

  onDeleteUser() {
    this.deleteUserStateSuccess = undefined;
    this.isDeleteLoading = true;
    this.userService.deleteUser(this.user.id!).subscribe({
      next: (response) => {
        this.deleteUserStateSuccess = true;
        this.isDeleteLoading = false;
        setTimeout(() => {
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/admin/users-management']);
          });
        }, 1200);
      },
      error: (error) => {
        console.error("Error deleting user:", error);
        this.deleteUserStateSuccess = false;
        this.isDeleteLoading = false;
      }
    });
  }
}
