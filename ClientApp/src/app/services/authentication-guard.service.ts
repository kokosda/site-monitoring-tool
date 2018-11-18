import { AuthenticationService } from './authentication.service';
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router'

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuardService implements CanActivate {
  private router: Router;
  private authenticationService: AuthenticationService;

  constructor(router: Router, authenticationService: AuthenticationService) {
    this.router = router;
    this.authenticationService = authenticationService;
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authenticationService.isLoggedIn()) {
        return true;
    }

    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
    return false;
  }
}
