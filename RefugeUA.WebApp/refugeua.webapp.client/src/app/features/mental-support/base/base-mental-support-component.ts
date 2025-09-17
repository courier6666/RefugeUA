import { Roles } from "../../../core/constants/user-roles-constants";
import { VolunteerEventType } from "../../../core/enums/volunteer-event-type-enum";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { AuthService } from "../../../core/services/auth/auth-service";

export abstract class BaseMentalSupportComponent {
    protected Math = Math;
    protected currentPage: number = 1;
    protected pages: number = 10;
    protected dataLoaded = false;
    protected isDisplayedFull = false;
    protected displayedCountMin = 5;
    protected dataFound = true;
    public Roles = Roles;
    public prompt?: string | null = null;

    constructor(protected router: Router,
        protected route: ActivatedRoute,
        public authService: AuthService) {

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

    abstract loadDataWithQuery(params: Params): void;

    abstract initializeDataFromQuery(params: Params): void;
}