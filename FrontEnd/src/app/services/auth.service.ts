import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginRequest } from '../models/login.request';
import { RegisterRequest } from '../models/register.request';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly _url = `${environment.apiUrl}/auth/`;

  constructor(
    private _http: HttpClient,
    private _snackBar: MatSnackBar,
    private _cookieService: CookieService,
    private _router: Router,
    private _socialAuthService: SocialAuthService,
  ) {}

  public setToken(token: string): void {
    this._cookieService.set('token', token);
  }

  public getToken(): string | null {
    return this._cookieService.get('token');
  }

  public getCurrentUser(): User | null {
    return JSON.parse(this._cookieService.get('user'));
  }

  public isAuthenticated(): boolean {
    return !!this.getToken();
  }

  public loginWithGoogle(socialUser: SocialUser): Observable<boolean> {
    return new Observable<boolean>((observer) => {
      this._http
        .post<any>(this._url + 'google', { idToken: socialUser.idToken }, { observe: 'response' })
        .subscribe({
          next: (response: HttpResponse<any>) => {
            this.setToken(response.body.authToken);
            let user: User = {
              email: socialUser.email,
              name: response.body.userName
            };
            this._cookieService.set('user', JSON.stringify(<User>user));
            this._snackBar.open('Welcome', 'Close', {
              duration: 3000,
            });
            observer.next(true);
            observer.complete();
          },
          error: () => {
            this._snackBar.open('No user found with specified email. You need to register first', 'Undo', {
              duration: 3000,
            });
            observer.next(false);
            observer.complete();
          },
        });
    });
  }

  public loginWithEmail(request: LoginRequest): Observable<boolean> {
    return new Observable<boolean>((observer) => {
      this._http
        .post<any>(this._url + 'email', request, { observe: 'response' })
        .subscribe({
          next: (response: HttpResponse<any>) => {
            this.setToken(response.body.authToken);
            let user: User = {
              email: request.email,
              name: response.body.userName
            };
            this._cookieService.set('user', JSON.stringify(<User>user));
            this._snackBar.open('Welcome', 'Close', {
              duration: 3000,
            });
            observer.next(response.ok);
            observer.complete();
          },
          error: () => {
            this._snackBar.open('Wrong email or password. Try again.', 'Undo', {
              duration: 3000,
            });
            observer.next(false);
            observer.complete();
          },
        });
    });
  }

  public register(request: RegisterRequest): Observable<boolean> {
    return new Observable<boolean>((observer) => {
      this._http
        .post<any>(this._url + 'register', request, { observe: 'response' })
        .subscribe({
          next: (response: HttpResponse<any>) => {
            this._snackBar.open(
              'Registration completed successfully. You can now log in',
              'Close',
              {
                duration: 4000,
              }
            );
            observer.next(response.ok);
            observer.complete();
          },
          error: () => {
            this._snackBar.open(`Registration failed`, 'Close', {
              duration: 3000,
            });
            observer.next(false);
            observer.complete();
          },
        });
    });
  }

  public logout(): void {
    this._cookieService.delete('token');
    this._router.navigate(['/login']);
    this._socialAuthService.signOut();
  }
}
