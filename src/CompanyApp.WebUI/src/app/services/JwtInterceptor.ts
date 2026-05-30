import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, switchMap } from 'rxjs/operators';
import { AuthService } from './auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.includes('/auth/gettoken')) {
      return next.handle(req);
    }

    const token = localStorage.getItem('token');

    const authReq = token
      ? req.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          }
        })
      : req;

    return next.handle(authReq).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 401) {
          return this.authService.GetToken().pipe(
            switchMap((response: any) => {
              localStorage.setItem('token', response.token);

              const retryReq = req.clone({
                setHeaders: {
                  Authorization: `Bearer ${response.token}`
                }
              });

              return next.handle(retryReq);
            })
          );
        }

        return throwError(() => error);
      })
    );
  }
}