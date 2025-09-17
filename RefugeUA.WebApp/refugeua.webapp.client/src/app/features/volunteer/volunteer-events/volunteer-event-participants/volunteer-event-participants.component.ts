import { Component } from '@angular/core';
import { User, VolunteerEvent } from '../../../../core/models';
import { Router, ActivatedRoute } from '@angular/router';
import { VolunteerEventService } from '../../../../core/services/volunteer/volunteer-event-service';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-volunteer-event-participants',
  standalone: false,
  templateUrl: './volunteer-event-participants.component.html',
  styleUrl: './volunteer-event-participants.component.css'
})
export class VolunteerEventParticipantsComponent {
  users: User[] = [];
  volunteerEvent!: VolunteerEvent;
  currentPage: number = 1;
  usersLoaded = false;
  usersFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  volunteerEventFoundSuccess?: boolean;
  isAllowedToEditEvent: boolean = false;
  subscription!: Subscription;

  constructor(private volunteerEventService: VolunteerEventService, private authService: AuthService, private router: Router, private route: ActivatedRoute) {

  }

  getNavigationCallback(): (page: number) => void {
    return (page: number) => this.onPageNavigation(page);
  }

  getOnSearchBarCallback(): (prompt: string | null) => void {
    return (prompt: string | null) => {
      this.prompt = prompt;
      this.currentPage = 1;
      this.loadUsers();
    }

  }

  getOnParticipantRemovedCallback(): (id: number) => void {
    return (id: number): void => {
      this.volunteerEventService.removeParticipantFromVolunteerEvent(this.volunteerEvent.id!, id).subscribe({
        next: (res) => {
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/volunteer/events', this.volunteerEvent.id!, 'participants']);
          });
        },
        error: (err) => {
          console.log("Error removing user: ", err);
        }
      });
    }
  }

  onPageNavigation(page: number) {
    window.scrollTo(0, 0);
    this.currentPage = page;

    let queryParams = { page: page };
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: queryParams,
      queryParamsHandling: 'merge'
    });
  }

  loadUsers() {

    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.usersLoaded = false;
    this.usersFound = undefined;
    this.subscription = this.volunteerEventService.getVolunteerEventParticipants(this.volunteerEvent.id!, this.currentPage, 10, this.prompt).subscribe({
      next: (response) => {
        this.users = response.items;
        this.pages = response.pagesCount;
        this.usersFound = response.totalCount > 0;
        this.usersLoaded = true;
      },
      error: (error) => {
        this.usersLoaded = true;
        this.usersFound = false;
      }
    });
  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;

    this.volunteerEventService.getVolunteerEvent(id).subscribe({
      next: (res) => {
        this.volunteerEventFoundSuccess = true;
        this.volunteerEvent = res;
        this.authService.isAllowedToEditVolunteerEvent(id).subscribe({
          next: (res) => this.isAllowedToEditEvent = res,
          error: (err) => {
            this.isAllowedToEditEvent = false;
            console.log(err);
          }
        });
        
        this.route.params.subscribe(params => {
          window.scrollTo(0, 0);
          if (params['page']) {
            this.currentPage = +params['page'];
          }
          else {
            this.currentPage = 1;
          }

          this.loadUsers();
        });
      },
      error: (err) => {
        this.volunteerEventFoundSuccess = false;
      }
    });

  }
}
