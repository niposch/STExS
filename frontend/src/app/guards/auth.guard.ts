import {Injectable} from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import { firstValueFrom } from 'rxjs';
import {UserService} from "../services/user.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private userService: UserService
  ) {
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let hasCookie = await firstValueFrom(this.userService.hasCookie());
    if (hasCookie) {
      // logged in so return true
      return true;
    }

    // not logged in so redirect to login page with the return url
    await this.router.navigate(['/login'], {queryParams: {callbackUrl: state.url}});
    return false;
  }
}
