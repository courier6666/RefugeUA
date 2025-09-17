import { Component, ViewChild, ElementRef, OnInit, OnDestroy } from '@angular/core';
import { MentalSupportArticle } from '../../../../core/models';
import { Router, ActivatedRoute } from '@angular/router';
import { mockMentalSupportArticles } from '../../../../shared/util/mock-up-data/mental-support-articles-ua';
import { Subscription } from 'rxjs';
import { Roles } from '../../../../core/constants/user-roles-constants';
import { AuthService } from '../../../../core/services/auth/auth-service';
import { MentalSupportArticleService } from '../../../../core/services/mental-support/mental-support-article-service';

@Component({
  selector: 'app-mental-support-article-detail',
  standalone: false,
  templateUrl: './mental-support-article-detail.component.html',
  styleUrl: './mental-support-article-detail.component.css'
})
export class MentalSupportArticleDetailComponent implements OnInit, OnDestroy {
  mentalSupportArticle?: MentalSupportArticle;
  Roles = Roles;
  @ViewChild('deletionConfirmation') deletionConfirmationRef!: ElementRef<HTMLDivElement>;
  isDeleted: boolean = false;
  isAuthenticated: boolean = false;
  isAuthenticatedSubscription!: Subscription;
  isAllowedToEdit: boolean = false;

  constructor(private router: Router,
    private route: ActivatedRoute,
    public authService: AuthService,
    public articleService: MentalSupportArticleService) {

  }

  onDelete() {
    this.deletionConfirmationRef.nativeElement.style.display = 'flex';
  }

  confirmDeletion() {
    let id = this.mentalSupportArticle?.id!;
    this.mentalSupportArticle = undefined;
    this.articleService.deleteArticle(id).subscribe({
      next: (res) => {
        this.isDeleted = true;
      },
      error: (err) => console.error(err)
    });
  }

  cancelDeletion() {
    this.deletionConfirmationRef.nativeElement.style.display = 'none';
  }

  ngOnInit(): void {
    let routeParams = this.route.snapshot.paramMap;
    let id = +routeParams.get('id')!;

    this.articleService.getArticleById(id).subscribe({
      next: (res) => {
        this.mentalSupportArticle = res;
        this.isAuthenticatedSubscription = this.authService.authSubject.subscribe({
          next: (val) => {
            this.isAuthenticated = val;
            this.isAllowedToEdit = this.authService.userClaims.id === res.authorId?.toString() || this.authService.userClaims.role == Roles.Admin;
          }
        });
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  ngOnDestroy(): void {
    if (this.isAuthenticatedSubscription) {
      this.isAuthenticatedSubscription.unsubscribe();
    }
  }
}
