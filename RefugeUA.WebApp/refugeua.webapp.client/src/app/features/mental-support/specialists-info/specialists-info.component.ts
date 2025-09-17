import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { PsychologistInformation } from '../../../core/models';
import { mockPsychologists } from '../../../shared/util/mock-up-data/psychologist-information-ua';
import { BaseMentalSupportComponent } from '../base/base-mental-support-component';
import { AuthService } from '../../../core/services/auth/auth-service';
import { PsychologistInformationService } from '../../../core/services/mental-support/psychologist-information-service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-specialists-info',
  standalone: false,
  templateUrl: './specialists-info.component.html',
  styleUrl: './specialists-info.component.css'
})
export class SpecialistsInfoComponent extends BaseMentalSupportComponent implements OnInit {
  currentPsychologistInfos: PsychologistInformation[] = mockPsychologists;

  subscription!: Subscription;

  constructor(router: Router, route: ActivatedRoute, authService: AuthService, public psychInfoService: PsychologistInformationService) {
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

    if(this.subscription)
    {
      this.subscription.unsubscribe();
    }

    this.subscription = this.psychInfoService.getPsychologistInformations(query.page, query.pageLength, query.prompt).subscribe({
      next: (result) => {
        console.log(result);
        this.currentPsychologistInfos = result.items;
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
