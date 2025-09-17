import { Component } from '@angular/core';
import { Router, ActivatedRoute, Params, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../../../core/services/auth/auth-service';
import { Roles } from '../../../core/constants/user-roles-constants';
import { Announcement, AnnouncementGroup } from '../../../core/models';
import { AbstractControl } from '@angular/forms';

export abstract class BaseAnnouncementComponent {
  protected Math = Math;
  protected currentPage: number = 1;
  protected pages: number = 10;
  protected announcementsLoaded = false;
  protected isDisplayedFull = false;
  protected displayedCountMin = 5;
  protected announcementsFound = true;
  public Roles = Roles;
  public prompt?: string | null = null;
  public groups!: AnnouncementGroup[];
  public displayedGroups!: AnnouncementGroup[];

  constructor(protected router: Router, protected route: ActivatedRoute, public authService: AuthService) {

  }

  protected onPageNavigation(page: number) {
    window.scrollTo(0, 0);
    this.currentPage = page;
    let queryParams = { page: page };
    this.router.navigate([], {
      relativeTo: this.route,
      queryParams: queryParams,
      queryParamsHandling: 'merge'
    });
  }

  protected abstract get activatedRoute(): ActivatedRoute;

  protected getOnSearchBarCallback(): (prompt: string | null) => void {
    return (prompt: string | null) => {
      this.prompt = prompt;
      this.currentPage = 1;

      var queryParams = this.activatedRoute.snapshot.queryParams;
      console.log(this.activatedRoute);
      console.log(queryParams);

      queryParams = {
        prompt: this.prompt
      }

      this.router.navigate([], {
        relativeTo: this.activatedRoute,
        queryParams: queryParams,
        queryParamsHandling: 'merge'
      });
    }
  }

  protected getNavigationCallback(): (page: number) => void {
    return (page: number) => this.onPageNavigation(page);
  }

  abstract loadAnnouncementsWithQuery(params: Params): void;

  abstract onFormsDataChanged(): void

  abstract initializeForm(): void;

  abstract initializeDataFromQuery(params: Params): void;

  abstract get group(): AbstractControl<any, any> | null;

  public setDisplayedGroups(name: string)
  {
    this.displayedGroups = this.groups.filter(g => g.name.toUpperCase().includes(name?.toUpperCase() ?? ''));
  }

  public onAnnouncementGroupChosen(name: string) {
    console.log(1);
    this.group?.setValue(name);
  }
}
