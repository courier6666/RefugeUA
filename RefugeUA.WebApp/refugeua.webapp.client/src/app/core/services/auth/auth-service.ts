import { HttpClient } from "@angular/common/http";
import { CookieService } from "ngx-cookie-service";
import { catchError, map, Observable, of } from "rxjs";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { RegisterCommand } from "../../api-models/register-command";
import { throwError } from "rxjs";
import { SendEmailConfirmation } from "../../api-models/send-email-confirmation";
import { Token } from "../../api-models/token";
import { decodeToken } from "../../../shared/util/token-decoder/token-decode";
import { Router } from "@angular/router";
import { SessionStorageService } from "../util/session-storage-service";
import { UserClaims } from "../../api-models/user-claims";
import { catchErrorForFormMethod } from "../../../shared/util/form-error-capture-method";

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    authSubject = new BehaviorSubject<boolean>(false);
    isAdminSubject = new BehaviorSubject<boolean>(false);
    isCommunityAdminSubject = new BehaviorSubject<boolean>(false);
    isVolunteerSubject = new BehaviorSubject<boolean>(false);
    isLocalCitizenSubject = new BehaviorSubject<boolean>(false);
    isMilitaryOrFamilySubject = new BehaviorSubject<boolean>(false);

    public userClaims!: UserClaims;

    isAuthenticated$ = this.authSubject.asObservable();
    isAdmin$ = this.isAdminSubject.asObservable();
    isCommunityAdmin$ = this.isCommunityAdminSubject.asObservable();
    isVolunteer$ = this.isVolunteerSubject.asObservable();
    isLocalCitizen$ = this.isLocalCitizenSubject.asObservable();
    isMilitaryOrFamily$ = this.isMilitaryOrFamilySubject.asObservable();

    constructor(
        private cookieService: CookieService,
        private http: HttpClient,
        private router: Router,
        private sessionStorageService: SessionStorageService) {
    }

    register(registerCommand: RegisterCommand): Observable<string> {
        return this.http.post<any>('api/auth/register', registerCommand).pipe(
            map(response => {
                return response;
            }),
            catchError(err => {
                const statusCode = err.status;
                console.error('Error Status Code:', statusCode);

                let errorMessage = "";

                let errors: string[] = [];
                if (err.status == 400 && err.error.errors && Object.keys(err.error.errors).length > 0) {
                    for (let field in err.error.errors) {
                        errors.push(...err.error.errors[field]);
                    }
                    errorMessage = errors.join('\n');
                }
                console.log(errorMessage);

                return throwError(() => new Error(errorMessage));
            })
        );
    }

    authenticate() {
        let token = this.sessionStorageService.getItem('token');
        console.log(token);
        try {
            let claims = decodeToken(token!);
            this.userClaims = claims;
            if (claims.exp <= Math.floor(Date.now() / 1000) - 3600 * 3) {
                this.sessionStorageService.setItem('sessionExpired', 'true');
                this.sessionExpired();
                return;
            }

            this.authSubject.next(true);
            switch (claims.role) {
                case 'Admin':
                    this.isAdminSubject.next(true);
                    break;
                case 'CommunityAdmin':
                    this.isCommunityAdminSubject.next(true);
                    break;
                case 'Volunteer':
                    this.isVolunteerSubject.next(true);
                    break;
                case 'LocalCitizen':
                    this.isLocalCitizenSubject.next(true);
                    break;
                case 'MilitaryOrFamily':
                    this.isMilitaryOrFamilySubject.next(true);
                    break;
            }
        }
        catch (error) {
            this.sessionStorageService.removeItem('token');
        }
    }

    login(email: string, password: string): Observable<boolean> {

        return this.http.post<Token>('api/auth/login', {
            email: email,
            password: password
        }).pipe(map(response => {
            console.log(response);
            this.sessionStorageService.setItem('token', response.token);
            this.authenticate();
            return true;
        }),
            catchError(err => {
                let errorMessage: string = '';

                switch (err.status) {
                    case 500:
                        errorMessage = "Виникла помилка на сервері, спробуйте ще раз";
                        break;
                    case 404:
                        errorMessage = "Не було знайдено наступної пошти: " + email;
                        break;
                    case 400:
                        errorMessage = "Введений хибний пароль";
                        break;
                    case 403:
                        let errors: string[] = [];

                        if (err.error.detail.includes("Not accepted")) {
                            errors.push("Заявка на реєстрацію за цією поштою. Зачекайте повідомлення на пошту або спробуйте пізніше.");
                        }

                        if (err.error.detail.includes("Not confirmed")) {
                            errors.push("Дану пошту не було підтверджено, підтвердіть пошту.");
                        }

                        errorMessage = errors.join('\n');
                        break;
                }

                return throwError(() => new Error(errorMessage));
            }));
    }

    sessionExpired() {
        if (this.sessionStorageService.getItem('token')) {
            this.sessionStorageService.removeItem('token');
        }
        this.authSubject.next(false);
        this.isAdminSubject.next(false);
        this.isCommunityAdminSubject.next(false);
        this.isLocalCitizenSubject.next(false);
        this.isMilitaryOrFamilySubject.next(false);

        console.log(this.router.url);
        this.router.navigate(['/', 'login']);
    }

    logout() {

        if (this.sessionStorageService.getItem('token')) {
            this.sessionStorageService.removeItem('token');
        }

        this.authSubject.next(false);
        this.isAdminSubject.next(false);
        this.isCommunityAdminSubject.next(false);
        this.isLocalCitizenSubject.next(false);
        this.isMilitaryOrFamilySubject.next(false);

        console.log(this.router.url);
        this.router.navigate([this.router.url]);
    }

    sendEmailConfirmation(sendEmailConfirmation: SendEmailConfirmation): Observable<boolean> {
        return this.http.post(`api/auth/send-email-confirmation`, sendEmailConfirmation).
            pipe(map(response => {
                return true;
            }),
                catchError(err => {
                    return throwError(() => new Error('Виникла помилка відправлення підтвердження на пошту'));
                }));
    }

    confirmEmail(token: string, email: string): Observable<boolean> {
        return this.http.post(`api/auth/confirm-email?token=${token}&email=${email}`, {}).
            pipe(map(response => {
                return true;
            }),
                catchError(err => {
                    return throwError(() => new Error('Виникла помилка підтвердження пошти'));
                }));
    }

    emailExists(email: string): Observable<boolean> {
        return this.http.get<boolean>(`api/auth/email-exists?email=${email}`);
    }

    phoneNumberExists(phoneNumber: string): Observable<boolean> {
        const encodedPhoneNumber = encodeURIComponent(phoneNumber);
        return this.http.get<boolean>(`api/auth/phonenumber-exists?phoneNumber=${encodedPhoneNumber}`);
    }

    isAllowedToEditAnnouncement(id: number): Observable<boolean> {
        return this.http.get<boolean>(`api/announcements/${id}/is-allowed-to-edit`).
            pipe(map(response => {
                return response;
            }),
                catchError(err => {
                    return of(false);
                }));
    }

    isAllowedToEditVolunteerEvent(id: number): Observable<boolean> {
        return this.http.get<boolean>(`api/volunteer/events/${id}/is-allowed-to-edit`).
            pipe(map(response => {
                return response;
            }),
                catchError(err => {
                    return of(false);
                }));
    }

    isAllowedToEditVolunteerGroup(id: number): Observable<boolean> {
        return this.http.get<boolean>(`api/volunteer/groups/${id}/is-allowed-to-edit`);
    }

    IsAdminOfVolunteerGroup(id: number): Observable<boolean> {
        return this.http.get<boolean>(`api/volunteer/groups/${id}/am-admin-of-group`);
    }
}