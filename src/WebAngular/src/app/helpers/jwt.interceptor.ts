import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  accessTokenName = 'access_token';
  constructor(
    private toastr: ToastrService,
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    request = request.clone({
      headers: new HttpHeaders({
        'api-version': '1.0',
        Authorization: `Bearer ${localStorage[this.accessTokenName]}`
      }),
    });
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        this.toastr.error(err?.error?.Message);
        return throwError(err?.error?.Message);
      })
    );
  }
}
