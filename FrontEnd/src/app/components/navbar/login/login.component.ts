import { SocialAuthService, SocialUser } from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginRequest } from 'src/app/models/login.request';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-logIn',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(
    private _socialAuthService: SocialAuthService,
    private _authService: AuthService,
    private _router: Router
  ) {
    this._socialAuthService.authState.subscribe((user: SocialUser) => {
      if (user) {
        this._authService
          .loginWithGoogle(user)
          .subscribe((isSignedIn: boolean) => {
            if (isSignedIn) {
              this.loginForm.reset();
              this._router.navigate(['/postlist']);
            }
          });
      }
    });
  }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.loginForm = new FormGroup({
      email: new FormControl(null),
      password: new FormControl(null),
    });
  }

  onSubmit(formData: FormGroup) {
    let request: LoginRequest = {
      email: formData.value.email,
      password: formData.value.password,
    };
    this._authService
      .loginWithEmail(request)
      .subscribe((isSignedIn: boolean) => {
        if (isSignedIn) {
          this.loginForm.reset();
          this._router.navigate(['/postlist']);
        }
      });
  }
}
