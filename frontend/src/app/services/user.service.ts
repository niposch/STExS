import {Injectable} from '@angular/core';
import {BehaviorSubject, catchError, first, firstValueFrom, Observable, of} from "rxjs";
import {map} from "rxjs/operators";
import {AuthenticateService} from "../../services/generated/services/authenticate.service";
import jwtDecode from "jwt-decode";
import {ApplicationUser} from "../../services/generated/models/application-user";
import {RoleType} from "../../services/generated/models/role-type";

@Injectable({
  providedIn: 'root'
})

export class UserService {
  public currentUser: Observable<ApplicationUser | null>;
  public currentUserSubject: BehaviorSubject<ApplicationUser | null>;
  public currentRoles: Observable<Array<RoleType> | null>;
  public currentRolesSubject: BehaviorSubject<Array<RoleType> | null>;
  constructor(private readonly authService: AuthenticateService) {
    this.currentUserSubject = new BehaviorSubject<ApplicationUser | null>(null);
    this.currentRolesSubject = new BehaviorSubject<Array<RoleType> | null>(null);
    this.currentUser = this.currentUserSubject.asObservable();
    this.currentRoles = this.currentRolesSubject.asObservable();
    firstValueFrom(this.hasCookie()).then(hasCookie => {
    })
  }

  async login(email: string, password: string):Promise<void> {
    await firstValueFrom(this.authService.apiAuthenticateLoginPost({
      body:{
        email: email,
        password: password
      }
    }));
    await firstValueFrom(this.getUserDetails());
  }

  hasCookie():Observable<boolean>{
    return this.getUserDetails().pipe<any, boolean>(
      map(() => true),
      catchError(err => of(false)))
  }

  getUserDetails():Observable<void>{
    return this.authService.apiAuthenticateUserDetailsGet$Json().pipe(map(u =>{
      this.currentUserSubject.next(u?.user ?? null);
      this.currentRolesSubject.next(u?.roles ?? null);
    }));
  }

  async logout():Promise<void> {
    this.currentUserSubject.next(null);
    this.currentRolesSubject.next(null);
    await firstValueFrom(this.authService.apiAuthenticateLogoutPost());
  }
}
