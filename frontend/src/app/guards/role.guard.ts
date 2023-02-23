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
    console.log(roles);
    const requiredRole = route.data['requiredRoles'];
    console.log(requiredRole)
    // @ts-ignore
    if (roles.includes(requiredRole)) {
      // has the required role so return true
      return true;
    }

    // doesn't have the required role so redirect to notfound page
    await this.router.navigate(['/notfound']);
    return false;
  }
}

