import {Injectable} from '@angular/core';
import {BehaviorSubject, catchError, firstValueFrom, Observable, of} from "rxjs";
import {map} from "rxjs/operators";
import {AuthenticateService} from "../../services/generated/services/authenticate.service";
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
    firstValueFrom(this.hasCookie()).then(() => {
    })
  }

  async login(email: string, password: string):Promise<void> {
    try {
      await firstValueFrom(this.authService.apiAuthenticateLoginPost$Json({
        body:{
          email: email,
          password: password
        }
      }));
    } catch (e) {
      console.log(e);
    }
    await firstValueFrom(this.getUserDetails());
  }

  private hasCookieCache = false;
  hasCookie(forceLoad=false):Observable<boolean>{
    if(!forceLoad && this.hasCookieCache){
      return of(true);
    }
    return this.getUserDetails().pipe<any, boolean>(
      map(() => {
        this.hasCookieCache = true;
        return true;
      }),
      catchError(() => of(false)))
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
