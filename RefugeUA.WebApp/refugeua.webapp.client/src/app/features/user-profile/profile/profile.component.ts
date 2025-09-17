import { Component, Input, OnInit } from '@angular/core';
import { User } from '../../../core/models';
import { Roles } from '../../../core/constants/user-roles-constants';
import { AuthService } from '../../../core/services/auth/auth-service';
import { ActivatedRoute } from '@angular/router';
import { FileService } from '../../../core/services/util/file-service';
import { IsValueNullNanOrUndefined } from '../../../shared/util/value-not-null-undefined-or-nan-checker';
import { Image } from '../../../core/models';

@Component({
  selector: 'app-profile',
  standalone: false,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  @Input({required: true}) user!: User;
  @Input() viewFromMyProfile = true;

  profilePath?: string = undefined;

  constructor(public authService: AuthService, private route: ActivatedRoute, public fileService: FileService) {

  }

  ngOnInit(): void {

    this.profilePath = (this.user.profileImageUrl != undefined || this.user.profileImageUrl != null) ? this.fileService.getApiPath(this.user.profileImageUrl!) : 'assets/icons/no-profile-picture.svg';
  }

  get roleUkr(): string {
    switch(this.user.role)
    {
      case Roles.Admin:
        return 'Адміністратор';
      case Roles.CommunityAdmin:
        return 'Адміністратор громади';
      case Roles.LocalCitizen:
        return 'Місцевий житель';
      case Roles.MilitaryOrFamily:
        return "Військовий або член сім'ї військового";
      case Roles.Volunteer:
        return "Волонтер";
    }

    return '';
  }
}
