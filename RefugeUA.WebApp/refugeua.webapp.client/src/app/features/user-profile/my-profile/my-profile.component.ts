import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../core/services/user/user-service';
import { User } from '../../../core/models';

@Component({
  selector: 'app-my-profile',
  standalone: false,
  templateUrl: './my-profile.component.html',
  styleUrl: './my-profile.component.css'
})
export class MyProfileComponent implements OnInit {

  user!: User;

  userLoadedSuccess?: boolean = undefined;

  constructor(private userService: UserService) {

  }

  ngOnInit(): void {
    this.userLoadedSuccess = undefined;
    this.userService.getMyProfile().subscribe({
      next: (res) => {
        console.log(res);
        this.user = res;
        this.userLoadedSuccess = true;
      },
      error: (err) => {
        this.userLoadedSuccess = false;
      }
    });
  }
}
