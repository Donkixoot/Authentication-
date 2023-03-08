import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  displayName!: string | undefined;
  constructor(public _authService: AuthService, private _router: Router) {}

  ngOnInit() {
    let user = this._authService.getCurrentUser()?.name;
    if (user) {
      this.displayName = this._authService.getCurrentUser()?.name;
    }
  }

  isAuthenticated() {
    return this._authService.isAuthenticated();
  }

  logOut() {
    return this._authService.logout();
  }
}
