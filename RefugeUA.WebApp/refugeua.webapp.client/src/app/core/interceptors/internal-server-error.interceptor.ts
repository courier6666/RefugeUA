import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class InternalServerError implements HttpInterceptor
{
    constructor(private router: Router) {

    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        let cloneReq = req.clone();

        return next.handle(cloneReq).pipe(tap(() => { },
            (err: any) => {
                if (err instanceof HttpErrorResponse) {
                    if (err.status !== 500){
                        return;
                    }

                    this.router.navigateByUrl('/error/505');
                }
            }
        ));
    }
}
