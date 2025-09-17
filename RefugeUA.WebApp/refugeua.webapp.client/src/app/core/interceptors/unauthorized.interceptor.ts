import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Observable, tap } from 'rxjs';

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
    constructor(private router: Router) {

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        let cloneReq = req.clone();

        return next.handle(cloneReq).pipe(tap(() => { },
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    if (err.status !== 401){
                        return;
                    }

                    this.router.navigateByUrl('/login');
                }
            }
        ));
    }
}