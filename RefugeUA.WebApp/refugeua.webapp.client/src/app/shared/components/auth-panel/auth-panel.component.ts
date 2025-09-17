import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../core/services/auth/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth-panel',
  standalone: false,
  templateUrl: './auth-panel.component.html',
  styleUrl: './auth-panel.component.css'
})
export class AuthPanelComponent implements OnInit {
  constructor(public authService: AuthService, private router: Router)
  {
    
  }

  ngOnInit(): void {

  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);
  }
}
