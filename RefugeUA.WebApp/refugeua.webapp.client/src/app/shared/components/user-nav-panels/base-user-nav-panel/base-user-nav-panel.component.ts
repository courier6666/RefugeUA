import { Component } from '@angular/core';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { Roles } from '../../../../core/constants/user-roles-constants';

@Component({
  selector: 'app-base-user-nav-panel',
  standalone: false,
  templateUrl: './base-user-nav-panel.component.html',
  styleUrl: './base-user-nav-panel.component.css'
})
export class BaseUserNavPanelComponent {
  Roles = Roles

  constructor(public authService: AuthService) {

  }
}
