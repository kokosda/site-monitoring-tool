import { Component } from '@angular/core';
import { AuthenticationService } from './../services/authentication.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  private isExpanded = false;
  private authenticationService: AuthenticationService;

  constructor(authenticationService: AuthenticationService) {
    this.authenticationService = authenticationService;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  isLoggedIn() {
    let result = this.authenticationService.isLoggedIn();
    return result;
  }

  logOut() {
    this.authenticationService.logOut();
    this.collapse();
  }
}
