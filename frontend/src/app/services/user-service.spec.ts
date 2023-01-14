import { fakeAsync, flush, TestBed } from '@angular/core/testing';

import { UserService } from './user.service';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppComponent } from '../app.component';
import { firstValueFrom, of } from 'rxjs';
import { AuthenticateService } from 'src/services/generated/services';

describe('AuthenticationServiceService', () => {
  let expectedUser: any;
  let expectedRoles: any;

  let authServiceMock = {
    // this returns a promise
    apiAuthenticateUserDetailsGet$Json: () => {
      return of({ user: expectedUser, roles: expectedRoles });
    },
    apiAuthenticateLoginPost: () => {
      return of({});
    },
    apiAuthenticateLogoutPost: () => {},
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, HttpClientTestingModule],
      declarations: [AppComponent],
      providers: [{ provide: AuthenticateService, useValue: authServiceMock }],
    }).compileComponents();
  });

  let service: UserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call the backend on logout', fakeAsync(() => {
    // Arrange
    const spy = spyOn(
      authServiceMock,
      'apiAuthenticateLogoutPost' as never
    ).and.returnValue(of({}) as never);

    // Act
    service.logout();
    flush();

    // Assert
    expect(spy).toHaveBeenCalled();
  }));

  it('get user details should change the user/roles subjects', fakeAsync(() => {
    // Arrange
    expectedRoles = ['Admin'];
    expectedUser = {
      id: 1,
      email: 'email',
    };

    // Act
    firstValueFrom(service.getUserDetails());
    flush();

    // Assert
    expect(service.currentUserSubject.value).toEqual(expectedUser);
    expect(service.currentRolesSubject.value).toEqual(expectedRoles);
  }));

  it('login should call the login api route, call the getUserDetails route and update the user/roles subjects', fakeAsync(() => {
    // Arrange
    expectedRoles = ['Admin'];
    expectedUser = {
      id: 1,
      email: 'email',
    };

    // Act
    console.log(service);
    service.login('email', 'password');
    flush();

    // Assert
    expect(service.currentUserSubject.value).toEqual(expectedUser);
    expect(service.currentRolesSubject.value).toEqual(expectedRoles);
  }));
});
