import { TestBed } from '@angular/core/testing';

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

fdescribe('AuthGuard', () => {
  let guard: AuthGuard;
  let userService = jasmine.createSpyObj('UserService', ['hasCookie']);

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([
          { path: 'test', component: AppComponent },
        ]),
      ],
      providers: [{ provide: UserService, useValue: userService }],
    }).compileComponents();
    guard = TestBed.inject(AuthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('should return true when the cookie exists', () => {
    userService.hasCookie.and.returnValue(true);
    expect(guard.canActivate(null!, null!)).toBeTruthy();
  });

  it("should redirect to the login page if the cookie doesn't exist and pass the correct return url as a parameter", () => {
    // Arrange
    userService.hasCookie.and.returnValue(false);
    const router = TestBed.inject(Router);
    const route = new ActivatedRouteSnapshot();
    const state = {} as RouterStateSnapshot;
    state.url = '/test';
    const spy = spyOn(router, 'navigate' as never);

    // Act
    let res = guard.canActivate(route, state);
    expect(res).toBeFalsy();

    // Assert
    expect(spy).toHaveBeenCalledWith(
      ['/login'] as never,
      {
        queryParams: { callbackUrl: '/test' },
      } as never
    );
  });
});
