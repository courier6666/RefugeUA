import { Component } from '@angular/core';
import { VolunteerGroupService } from '../../../../core/services/volunteer/volunteer-group-services';
import { User, VolunteerGroup } from '../../../../core/models';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-volunteer-group-participants',
  standalone: false,
  templateUrl: './volunteer-group-participants.component.html',
  styleUrl: './volunteer-group-participants.component.css'
})
export class VolunteerGroupParticipantsComponent {
  users: User[] = [];
  volunteerGroup!: VolunteerGroup;
  currentPage: number = 1;
  usersLoaded = false;
  usersFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  groupFoundSuccess?: boolean;
  isAllowedToEditGroup: boolean = false;
  subscription!: Subscription;

  constructor(private volunteerGroupService: VolunteerGroupService, private authService: AuthService, private router: Router, private route: ActivatedRoute) {

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
      this.volunteerGroupService.removeFollowerFromVolunteerGroup(this.volunteerGroup.id!, id).subscribe({
        next: (res) => {
          this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/volunteer/groups', this.volunteerGroup.id!, 'followers']);
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
    this.subscription = this.volunteerGroupService.getVolunteerGroupFollowers(this.volunteerGroup.id!, this.currentPage, 10, this.prompt).subscribe({
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

    this.volunteerGroupService.getVolunteerGroupById(id).subscribe({
      next: (res) => {
        this.groupFoundSuccess = true;
        this.volunteerGroup = res;
        this.authService.isAllowedToEditVolunteerGroup(id).subscribe({
          next: (res) => this.isAllowedToEditGroup = res,
          error: (err) => {
            this.isAllowedToEditGroup = false;
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
        this.groupFoundSuccess = false;
      }
    });

  }
}
