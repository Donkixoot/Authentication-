import {
  HttpClient,
  HttpHeaders,
  HttpResponse,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Post } from '../models/post';
import { PostRequest } from '../models/post.request';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class PostService {
  private readonly _url = `${environment.apiUrl}/post/`;

  constructor(
    private _http: HttpClient,
    private _authService: AuthService,
    private _snackBar: MatSnackBar
  ) {}

  public getAllPosts(email: string): Observable<Post[]> {
    return new Observable<Post[]>((observer) => {
      this._http
        .post<HttpResponse<any>>(
          this._url + 'all',
          { email: email },
          this.getHttpOptions()
        )
        .subscribe({
          next: (response: HttpResponse<any>) => {
            observer.next(response.body);
            observer.complete();
          },
          error: (response: HttpResponse<any>) => {
            this._snackBar.open('Something went wrong.', 'Undo', {
              duration: 3000,
            });
            this.validateToken(response);
            observer.next([]);
            observer.complete();
          },
        });
    });
  }

  public getPost(postId: number): Observable<Post> {
    return new Observable<Post>((observer) => {
      this._http
        .get<HttpResponse<any>>(this._url + `${postId}`, this.getHttpOptions())
        .subscribe({
          next: (response: HttpResponse<any>) => {
            observer.next(response.body);
            observer.complete();
          },
          error: (response: HttpResponse<any>) => {
            this._snackBar.open('Something went wrong.', 'Undo', {
              duration: 3000,
            });
            this.validateToken(response);
            observer.complete();
          },
        });
    });
  }

  public deletePost(postId: number): Observable<boolean> {
    return new Observable<boolean>((observer) => {
      this._http
        .delete<HttpResponse<any>>(
          this._url + `${postId}`,
          this.getHttpOptions()
        )
        .subscribe({
          next: () => {
            this._snackBar.open('Post deleted successfully.', 'Undo', {
              duration: 3000,
            });
            observer.next(true);
            observer.complete();
          },
          error: (response: HttpResponse<any>) => {
            this._snackBar.open('Something went wrong.', 'Undo', {
              duration: 3000,
            });
            this.validateToken(response);
            observer.next(false);
            observer.complete();
          },
        });
    });
  }

  public getHttpOptions() {
    const token = this._authService.getToken();
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });
    const options = {
      headers: headers,
      observe: 'response' as 'body',
    };

    return options;
  }

  public validateToken(response: HttpResponse<any>): void {
    if (response.status == 401) {
      this._authService.logout();
    }
  }

  public addOrUpdatePost(post: PostRequest): Observable<void> {
    return new Observable<void>((observer) => {
      this._http
        .post<any>(this._url + 'update', post, this.getHttpOptions())
        .subscribe({
          next: (response: HttpResponse<any>) => {
            this._snackBar.open('Post updated successfully', 'Close', {
              duration: 3000,
            });
            observer.next();
            observer.complete();
          },
          error: (response: HttpResponse<any>) => {
            this._snackBar.open('Something went wrong.', 'Undo', {
              duration: 3000,
            });
            this.validateToken(response);
            observer.next();
            observer.complete();
          },
        });
    });
  }
}
