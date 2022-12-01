import {Injectable} from '@angular/core';
import {BehaviorSubject, Observable} from "rxjs";
import {map} from "rxjs/operators";
import {AuthenticateService} from "../../services/generated/services/authenticate.service";
import jwtDecode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})

export class UserService {
  public currentUser: Observable<User | null>;
  public currentUserSubject: BehaviorSubject<User | null>;
  public currentUserModules: number[] = [0, 1, 3];
  constructor(private readonly authService: AuthenticateService) {
    let user = JSON.parse(localStorage.getItem('currentUser') ?? "null");
    if (user == null) {
      user = null;
    }
    this.currentUserSubject = new BehaviorSubject<User | null>(user);
    this.currentUser = this.currentUserSubject.asObservable();
  }

  login(email: string, password: string) {
    return this.authService.apiAuthenticateLoginPost$Json({
      body: {
        email: email,
        password: password
      }
    })
      .pipe(map(tokens => {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          let decodedToken: AccessToken
          try {
            decodedToken = jwtDecode<AccessToken>(tokens.accessToken ?? "");
            console.log(decodedToken);
          } catch (e) {
            console.log(e);
            return null;
          }
          let user: User = {
            accessToken: tokens.accessToken!,
            refreshToken: tokens.refreshToken!,
            email: decodedToken.email,
            firstName: decodedToken.firstName,
            lastName: decodedToken.lastName,
            id: decodedToken.sub,
            userName: decodedToken.userName
          };
          localStorage.setItem('currentUser', JSON.stringify(user));
          this.currentUserSubject.next(user);
          return user;
        }),
      );
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}

interface User {
  userName: string;
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  accessToken: string;
  refreshToken: string;
}

interface AccessToken {
  userName: string;
  sub: string; // The subject of the token. This is the user id.
  email: string;
  firstName: string;
  lastName: string;
  exp: number;
  iat: number;
}
