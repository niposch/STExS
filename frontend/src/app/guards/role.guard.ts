import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {firstValueFrom} from 'rxjs';
import {UserService} from "../services/user.service";

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(
    private router: Router,
    private userService: UserService
  ) {
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let roles = await firstValueFrom(this.userService.currentRoles);
    const requiredRole = route.data['requiredRoles'];

    if (roles!.some(item => requiredRole.includes(item))) {
      return true;
    }
    // doesn't have the required role so redirect to notfound page
    await this.router.navigate(['/forbidden']);
    return false;
  }
}

