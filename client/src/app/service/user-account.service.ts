import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from '../model/user.model';
import { StorageService } from './storage.service';

@Injectable({
  providedIn: 'root'
})
export class UserAccountService {

  public redirectUrl = '';
  readonly API_URL = 'http://localhost:24373/api/User';
  reply: Observable<User>
  readonly headers = new HttpHeaders({ 'Content-Type': 'application/json;' });

  constructor(
    private router: Router,
    private http: HttpClient,
    private storageService: StorageService
  ) { }

  private static handleError(error: HttpErrorResponse): any {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError(
      'Something bad happened; please try again later.');
  }

  login(username: string, password: string): Observable<any> {
    this.storageService.removeLoginUser();
    let loginData = {
      "username": username,
      "password": password
    };

    return this.http.post<User>(`${this.API_URL}/Login/`, loginData, { headers: this.headers })
      .pipe(tap(res => {
        this.storageService.saveLoginUser(res);
      }), catchError(UserAccountService.handleError));
  }

  logout() {
    this.storageService.removeLoginUser();
    this.router.navigate(['/login']).then(_ => console.log('Logout success.'));
  }
}
