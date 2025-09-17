import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth-service';

@Component({
  selector: 'app-confirm-email',
  standalone: false,
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.css'
})
export class ConfirmEmailComponent implements OnInit {

  result: Boolean | null = null;
  errorReason: string = '';

  constructor(private router: Router, private route: ActivatedRoute, private authService: AuthService) {

  }

  ngOnInit(): void {
    let queryParams = this.route.snapshot.queryParamMap;
    console.log(queryParams.get('token')!.replaceAll(' ', '+'));

    this.authService.confirmEmail(queryParams.get('token')!.replaceAll(' ', '+'), queryParams.get('email')!).
      subscribe({
        next: (result) => this.result = true,
        error: (err) => this.result = false
      });
  }
}
