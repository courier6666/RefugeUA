import { Component, OnDestroy, OnInit } from '@angular/core';
import { VolunteerGroup } from '../../../../core/models';
import { mockVolunteerGroup } from '../../../../shared/util/mock-up-data/mock-volunteer-group-ua';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { VolunteerGroupService } from '../../../../core/services/volunteer/volunteer-group-services';
import { ElementRef, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-volunteer-group-detail',
  standalone: false,
  templateUrl: './volunteer-group-detail.component.html',
  styleUrl: './volunteer-group-detail.component.css'
})
export class VolunteerGroupDetailComponent implements OnInit, OnDestroy {
  volunteerGroup?: VolunteerGroup;
  @ViewChild('deletionConfirmation') deletionConfirmationRef!: ElementRef<HTMLDivElement>;
  isDeleted: boolean = false;
  isAuthenticated: boolean = false;
  isAuthenticatedSubscription!: Subscription;
  isAllowedToEdit: boolean = false;
  isUserFollowing?: boolean = undefined;
  isAdmin: boolean = false;

  constructor(private router: Router,
    private route: ActivatedRoute,
    public authService: AuthService,
    private volunteerGroupService: VolunteerGroupService) {

  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;

    this.isAuthenticatedSubscription = this.authService.authSubject.subscribe({
      next: (val) => {
        this.isAuthenticated = val;
        this.authService.isAllowedToEditVolunteerGroup(id).subscribe({
          next: (res) => {
            this.isAllowedToEdit = res;
          }
        });
        if (val) {
          this.volunteerGroupService.meFollowingGroup(id).subscribe({
            next: (res) => this.isUserFollowing = res,
            error: (err) => console.log(err)
          });


        }
        
      }
    });

    this.volunteerGroupService.getVolunteerGroupById(id).subscribe({
      next: (result) => {
        console.log(result);
        this.volunteerGroup = result;
        this.authService.IsAdminOfVolunteerGroup(id).subscribe({
          next: (res) => this.isAdmin = res,
          error: (err) => {
            console.log(err);
          }
        });
      },
      error: (err) => console.log(err)
    });
  }

  onDelete() {
    this.deletionConfirmationRef.nativeElement.style.display = 'flex';
  }

  confirmDeletion() {
    let id = this.volunteerGroup?.id!;
    this.volunteerGroup = undefined;
    this.volunteerGroupService.deleteById(id).subscribe({
      next: (res) => {
        this.isDeleted = true;
      },
      error: (err) => console.error(err)
    });
  }

  cancelDeletion() {
    this.deletionConfirmationRef.nativeElement.style.display = 'none';
  }

  ngOnDestroy(): void {
    if (this.isAuthenticatedSubscription) {
      this.isAuthenticatedSubscription.unsubscribe();
    }
  }

  onLeave() {
    this.isUserFollowing = undefined;
    this.volunteerGroupService.leaveVolunteerGroup(this.volunteerGroup!.id!).subscribe({
      next: (res) => this.isUserFollowing = false,
      error: (err) => console.log(err)
    });
  }

  onFollowed() {
    this.isUserFollowing = undefined;
    this.volunteerGroupService.followVolunteerGroup(this.volunteerGroup!.id!).subscribe({
      next: (res) => this.isUserFollowing = true,
      error: (err) => console.log(err)
    });
  }
}
