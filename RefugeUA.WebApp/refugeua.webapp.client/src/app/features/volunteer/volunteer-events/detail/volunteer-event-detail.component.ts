import { Component, OnDestroy, OnInit } from '@angular/core';
import { VolunteerEvent } from '../../../../core/models';
import { VolunteerEventType } from '../../../../core/enums/volunteer-event-type-enum';
import { mockVolunteerEvent } from '../../../../shared/util/mock-up-data/volunteer-event';
import { Router, ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ElementRef } from '@angular/core';
import { ViewChild } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { VolunteerEventService } from '../../../../core/services/volunteer/volunteer-event-service';

@Component({
  selector: 'app-volunteer-event-detail',
  standalone: false,
  templateUrl: './volunteer-event-detail.component.html',
  styleUrl: './volunteer-event-detail.component.css',
})
export class VolunteerEventDetailComponent implements OnInit, OnDestroy {
  readonly VolunteerEventType = VolunteerEventType;
  volunteerEvent?: VolunteerEvent;
  @ViewChild('deletionConfirmation') deletionConfirmationRef!: ElementRef<HTMLDivElement>;
  isDeleted: boolean = false;
  isAuthenticated: boolean = false;
  isAuthenticatedSubscription!: Subscription;
  isAllowedToEdit: boolean = false;
  isUserParticipating?: boolean = undefined;

  openSubscription!: Subscription;
  closeSubscription!: Subscription;

  constructor(private router: Router,
    private route: ActivatedRoute,
    public authService: AuthService,
    private volunteerEventService: VolunteerEventService) {

  }

  onDelete() {
    this.deletionConfirmationRef.nativeElement.style.display = 'flex';
  }

  confirmDeletion() {
    let id = this.volunteerEvent?.id!;
    this.volunteerEvent = undefined;
    this.volunteerEventService.deleteById(id).subscribe({
      next: (res) => {
        this.isDeleted = true;
      },
      error: (err) => console.error(err)
    });
  }

  cancelDeletion() {
    this.deletionConfirmationRef.nativeElement.style.display = 'none';
  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;

    this.isAuthenticatedSubscription = this.authService.authSubject.subscribe({
      next: (val) => {
        this.isAuthenticated = val;
        this.authService.isAllowedToEditVolunteerEvent(id).subscribe({
          next: (res) => {
            this.isAllowedToEdit = res;
          }
        });
        if (val) {
          this.volunteerEventService.meParticipatingInEvent(id).subscribe({
            next: (res) => this.isUserParticipating = res,
            error: (err) => console.log(err)
          });
        }
      }
    });

    this.volunteerEventService.getVolunteerEvent(id).subscribe({
      next: (result) => {
        console.log(result);
        this.volunteerEvent = result;
        console.log(result.scheduleItems);
        console.log(result.scheduleItems?.length);
      },
      error: (err) => console.log(err)
    });


  }

  ngOnDestroy(): void {
    if (this.isAuthenticatedSubscription) {
      this.isAuthenticatedSubscription.unsubscribe();
    }
  }

  get isAuthUserAnOrganizer() {
    return this.volunteerEvent?.organizers.map(o => o.id?.toString()).includes(this.authService.userClaims.id);
  }

  onLeave() {
    this.isUserParticipating = undefined;
    this.volunteerEventService.leaveFromVolunteerEvent(this.volunteerEvent!.id!).subscribe({
      next: (res) => {
        this.isUserParticipating = false;
        this.volunteerEvent!.participantsCount! -= 1;
      },
      error: (err) => console.log(err)
    });
  }

  onParticipated() {
    this.isUserParticipating = undefined;
    this.volunteerEventService.participateInVolunteerEvent(this.volunteerEvent!.id!).subscribe({
      next: (res) => {
        this.isUserParticipating = true;
        this.volunteerEvent!.participantsCount! += 1;
      },
      error: (err) => console.log(err)
    });
  }

  onOpen(): void {
    if (this.openSubscription) {
      this.openSubscription.unsubscribe();
    }

    this.openSubscription = this.volunteerEventService.openVolunteerEvent(this.volunteerEvent?.id!).subscribe({
      next: () => {
        if (this.volunteerEvent)
          this.volunteerEvent.isClosed = false;
      },
      error: (error) => {
        console.error("Error opening volunteer event:", error);
      }
    });
  }

  onClosed(): void {
    if (this.closeSubscription) {
      this.closeSubscription.unsubscribe();
    }

    this.closeSubscription = this.volunteerEventService.closeVolunteerEvent(this.volunteerEvent?.id!).subscribe({
      next: () => {
        if (this.volunteerEvent)
          this.volunteerEvent.isClosed = true;
      },
      error: (error) => {
        console.error("Error closing volunteer event:", error);
      }
    });
  }
}
