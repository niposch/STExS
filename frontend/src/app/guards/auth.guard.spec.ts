import {fakeAsync, flush, TestBed, tick} from '@angular/core/testing';

import { AuthGuard } from './auth.guard';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AppComponent } from '../app.component';
import { UserService } from '../services/user.service';
import {
  ActivatedRouteSnapshot,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import {Observable, of} from 'rxjs';
import createSpy = jasmine.createSpy;

describe('AuthGuard', () => {
  let guard: AuthGuard;
  let userService = jasmine.createSpyObj('UserService', ['hasCookie']);
  let routerSpy = {navigate:createSpy('navigate')}

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
      ],
      providers: [{ provide: UserService, useValue: userService }, {provide: Router, useValue: routerSpy}],
    }).compileComponents();
    guard = TestBed.inject(AuthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('should return true when the cookie exists', fakeAsync(() => {
    userService.hasCookie.and.returnValue(of(true));
    guard.canActivate(null!, null!).then(v =>
      expect(v).toBeTruthy())
    tick();
  }));

  it("should redirect to the login page if the cookie doesn't exist and pass the correct return url as a parameter", fakeAsync(() => {
    // Arrange
    userService.hasCookie.and.returnValue(new Observable(e => {
      e.next(false);
      e.complete();
    }));
    const route = new ActivatedRouteSnapshot();
    const state = {} as RouterStateSnapshot;
    state.url = '/test';

    // Act
    guard.canActivate(route, state).then((result) => {
      expect(result).toBeFalsy();
    });
    tick();

    // Assert
    expect(routerSpy.navigate).toHaveBeenCalledWith(
      ['/login'] as never,
      {
        queryParams: { callbackUrl: '/test' },
      } as never
    );
  }));
});
