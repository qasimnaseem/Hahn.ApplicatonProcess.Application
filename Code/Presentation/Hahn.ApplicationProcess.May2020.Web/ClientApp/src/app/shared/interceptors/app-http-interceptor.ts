import { catchError, tap, finalize } from 'rxjs/internal/operators';
import { Injectable, isDevMode, Inject } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { NgxSpinnerService } from "ngx-spinner";
import { Router } from '@angular/router';
import { getBaseUrl } from 'src/main';
import { AppConstants } from '../app-constants/app-constants';

@Injectable({
  providedIn: 'root'
})

export class AppHttpInterceptor implements HttpInterceptor {

  private noOfRequests = 0;
  private baseUrl: string;

  constructor(
    private spinner: NgxSpinnerService,
    @Inject('BASE_URL') baseUrl: string
  ) { 
    this.baseUrl = baseUrl;
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    request = request.clone({ url: this.baseUrl + request.url });

    if (this.noOfRequests === 0) {
      this.spinner.show();
    }

    this.noOfRequests++;

    return next.handle(request).pipe(

      tap(data => { if (isDevMode()) { console.log(data); } }),

      finalize(() => {

        this.noOfRequests--;

        if (this.noOfRequests === 0) {  
          this.spinner.hide();
        }
      }),

      catchError((errorResponse: HttpErrorResponse) => {

        let errorMsg = AppConstants.ErrorMsgs.GenericError;

        if (errorResponse.error instanceof ErrorEvent) {
          errorMsg = errorResponse.error.message ? errorResponse.error.message : errorMsg
        }
        else {
          errorMsg = errorResponse.error.error ? errorResponse.error.error : errorMsg;
        }        
        
        this.notifyError(errorMsg);
        return throwError(errorResponse);
      })
    );
  }

  private notifyError(message: string) {
    console.log(message);
  }
}
