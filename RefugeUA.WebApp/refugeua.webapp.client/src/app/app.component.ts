import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/services/auth/auth-service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'RefugeUA';

  constructor(public authService: AuthService) {

  }

  ngOnInit(): void {
    this.authService.authenticate();
  }
}
