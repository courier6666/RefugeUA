import { Component, Input } from '@angular/core';
import { User } from '../../../core/models';
import { Roles } from '../../../core/constants/user-roles-constants';
import { AuthService } from '../../../core/services/auth/auth-service';
import { UserService } from '../../../core/services/user/user-service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-view-profile',
  standalone: false,
  templateUrl: './view-profile.component.html',
  styleUrl: './view-profile.component.css'
})
export class ViewProfileComponent {

  user!: User;

  userLoadedSuccess?: boolean = undefined;

  constructor(private userService: UserService, private router: Router, private route: ActivatedRoute) {

  }

  ngOnInit(): void {

    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;

    this.userLoadedSuccess = undefined;
    this.userService.getUserProfile(id).subscribe({
      next: (res) => {
        this.user = res;
        console.log(res);
        this.userLoadedSuccess = true;
      },
      error: (err) => {
        this.userLoadedSuccess = false;
      }
    });
  }

}
