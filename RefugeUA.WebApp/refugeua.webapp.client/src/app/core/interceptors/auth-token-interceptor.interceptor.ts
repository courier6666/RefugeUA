import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { SessionStorageService } from '../services/util/session-storage-service';

@Injectable()
export class AuthTokenInterceptor implements HttpInterceptor
{
  constructor(private cookieService: CookieService, private sessionStorageService: SessionStorageService) {
    
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let token = this.sessionStorageService.getItem('token');

    if(!token)
    {
      return next.handle(req);
    }

    let reqWithToken = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });

    return next.handle(reqWithToken);
  }
}
