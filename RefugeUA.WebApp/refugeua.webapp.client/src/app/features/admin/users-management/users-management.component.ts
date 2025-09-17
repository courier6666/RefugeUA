import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user/user-service';
import { User } from '../../../core/models';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-users-management',
  standalone: false,
  templateUrl: './users-management.component.html',
  styleUrl: './users-management.component.css'
})
export class UsersManagementComponent implements OnInit {

  users: User[] = [];
  currentPage: number = 1;
  usersLoaded = false;
  usersFound?: boolean;
  pages: number = 10;
  prompt?: string | null = null;
  subscription!: Subscription;

  constructor(private userService: UserService, private router: Router, private route: ActivatedRoute) {

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
    this.subscription = this.userService.getAllUsersAdmin(this.currentPage, 7, this.prompt).subscribe({
      next: (response) => {
        this.users = response.items;
        this.pages = response.pagesCount;
        this.usersFound = response.totalCount > 0;
        this.usersLoaded = true;
        console.log(response);
      },
      error: (error) => {
        this.usersLoaded = true;
        this.usersFound = false;
      }
    });
  }

  ngOnInit(): void {
    
    this.route.queryParams.subscribe(params => {
      if(params['page'])
      {
        this.currentPage = +params['page'];
      }

      this.loadUsers();
    });
  }
}
