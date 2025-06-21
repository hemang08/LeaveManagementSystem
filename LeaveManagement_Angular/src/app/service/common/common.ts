import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams, HttpStatusCode } from '@angular/common/http';
import { Observable, catchError, of, tap, Subject, BehaviorSubject, throwError } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { environment } from '../../../enviroment/enviroment';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})

export class CommonService {

  private userImageSource = new BehaviorSubject<string | null>('');
  userImage$ = this.userImageSource.asObservable();
  autoHide: number = 2000;
  private logoutSubject = new Subject<void>();
  public logoutAction$ = this.logoutSubject.asObservable();
  public href: any;
  IsError: boolean = false;
  isSystemAlert: boolean = false;

  constructor(
    private readonly toster: ToastrService,
    private readonly dialog: MatDialog,
    private readonly http: HttpClient) {
  }

  doGet(ApiUrl: String): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders()
    };
    const url = `${environment.ApiUrl}${ApiUrl}`;
    return this.http.get<any>(url, httpOptions).pipe(
      tap(() => this.log(`doGet success`)),
      catchError((error: HttpErrorResponse) => {
        this.checkAuthorize(error);
        return throwError(() => error);
      })
    );
  }

  doGetForDashboard(ApiUrl: String, options?: { params?: HttpParams }): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders()
    };
    let loginData = localStorage.getItem('authToken');
    if (loginData) {
      httpOptions.headers = httpOptions.headers.set('Authorization', 'Bearer ' + loginData);
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Origin', '*');
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Methods', 'POST, GET, OPTIONS, DELETE, PUT');
    }
    const combinedOptions = { ...httpOptions, ...options };
    const url = `${environment.ApiUrl}${ApiUrl}`;
    return this.http.get<any>(url, combinedOptions).pipe(
      tap(() => this.log(`doGet success`)),
      catchError((error: HttpErrorResponse) => {
        this.checkAuthorize(error);
        return throwError(() => error);
      })
    );
  }

  doPost(ApiUrl: string, postData: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders(
      )
    };
    let loginData = localStorage.getItem('authToken');
    if (loginData) {
      httpOptions.headers = httpOptions.headers.set('Authorization', 'Bearer ' + loginData);
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Origin', '*');
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Methods', 'POST, GET, OPTIONS, DELETE, PUT');
    }
    const url = `${environment.ApiUrl}${ApiUrl}`;
    return this.http.post<any>(url, postData, httpOptions).pipe(
      tap(() => this.log(`doGet success`)),
      catchError((error: HttpErrorResponse) => {
        this.checkAuthorize(error);
        return throwError(() => error);
      })
    );
  }

  doPut(ApiUrl: string, putData: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders(
      )
    };
    let loginData = localStorage.getItem('authToken');
    if (loginData) {
      httpOptions.headers = httpOptions.headers.set('Authorization', 'Bearer ' + loginData);
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Origin', '*');
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Methods', 'POST, GET, OPTIONS, DELETE, PUT');
    }
    const url = `${environment.ApiUrl}${ApiUrl}`;
    return this.http.put<any>(url, putData, httpOptions).pipe(
      tap(() => this.log(`doGet success`)),
      catchError((error: HttpErrorResponse) => {
        this.checkAuthorize(error);
        return throwError(() => error);
      })
    );
  }

  doDownloadPost(ApiUrl: string, postData: any) {
    const httpOptions = {
      headers: new HttpHeaders()
    };
    let loginData = localStorage.getItem('authToken');
    if (loginData) {
      httpOptions.headers = httpOptions.headers.set('Authorization', 'Bearer ' + loginData);
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Methods', '*');
    }
    const url = `${environment.ApiUrl}${ApiUrl}`;
    return this.http.post(url, postData, { headers: httpOptions.headers, observe: "response", responseType: "blob" });
  }

  doDelete(ApiUrl: String, idtoDelete: any): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders(
      )
    };
    let loginData = localStorage.getItem('authToken');
    if (loginData) {
      httpOptions.headers = httpOptions.headers.set('Authorization', 'Bearer ' + loginData);
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Origin', '*');
      httpOptions.headers = httpOptions.headers.set('Access-Control-Allow-Methods', 'POST, GET, OPTIONS, DELETE, PUT');
    }
    const options = {
      headers: httpOptions.headers,
      body: {
        UserId: idtoDelete
      }
    };
    const url = (`${environment.ApiUrl}${ApiUrl}`);
    return this.http.delete<any>(url, httpOptions).pipe(
      tap(() => this.log(`doGet success`)),
      catchError((error: HttpErrorResponse) => {
        this.checkAuthorize(error);
        return throwError(() => error);
      })
    );
  }

  // Check Authorize Role
  checkAuthorize(error: any) {
    console.log(error);
    this.dialog.closeAll();
    if (error.status == HttpStatusCode.Unauthorized) {
      if (!this.IsError) {
        this.IsError = true
        this.dialog.closeAll();
      }

    }
    else {
      if (!this.IsError) {
        this.IsError = true
        // this.toster.error("Something went wrong");
        let errorMessage = (error.error.message != null) ? error.error.message : error.message
      }
    }
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    this.IsError = false;
  }
}