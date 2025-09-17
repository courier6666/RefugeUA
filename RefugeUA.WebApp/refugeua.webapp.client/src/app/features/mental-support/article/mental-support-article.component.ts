import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { MentalSupportArticle } from '../../../core/models';
import { mockMentalSupportArticles } from '../../../shared/util/mock-up-data/mental-support-articles-ua';
import { AuthService } from '../../../core/services/auth/auth-service';
import { BaseMentalSupportComponent } from '../base/base-mental-support-component';
import { MentalSupportArticleService } from '../../../core/services/mental-support/mental-support-article-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-mental-support-article',
  standalone: false,
  templateUrl: './mental-support-article.component.html',
  styleUrl: './mental-support-article.component.css'
})
export class MentalSupportArticleComponent extends BaseMentalSupportComponent implements OnInit {

  currentArticles: MentalSupportArticle[] = mockMentalSupportArticles;
  subscription!: Subscription;

  constructor(router: Router, route: ActivatedRoute, authService: AuthService, public articleService: MentalSupportArticleService) {
    super(router, route, authService);
  }

  override get activatedRoute(): ActivatedRoute {
    return this.route;
  }

  override loadDataWithQuery(params: Params): void {
    this.dataLoaded = false;
    let query = { pageLength: 12, page: 1, prompt: null };

    if (params['page']) {
      query.page = +params['page'];
    }

    if (params['prompt']) {
      query.prompt = params['prompt'];
    }

    if (this.subscription) {
      this.subscription.unsubscribe();
    }

    this.subscription = this.articleService.getArticles(query.page, query.pageLength, query.prompt).subscribe({
      next: (result) => {
        console.log(result);
        this.currentArticles = result.items;
        this.pages = result.pagesCount;
        this.dataLoaded = true;
        this.dataFound = true;
      },
      error: (err) => {
        this.dataLoaded = true;
        this.dataFound = false;
        console.log(err);
      }
    });
  }

  override initializeDataFromQuery(params: Params): void {
    if (params['page']) {
      this.currentPage = +params['page'];
    }

    if (params['prompt']) {
      this.prompt = params['prompt'];
    }
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      window.scrollTo(0, 0);
      this.loadDataWithQuery(params);
    });
  }
}
