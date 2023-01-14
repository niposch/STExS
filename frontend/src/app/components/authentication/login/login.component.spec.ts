import {
  async,
  ComponentFixture,
  fakeAsync,
  TestBed,
  tick,
} from '@angular/core/testing';

import { LoginComponent } from './login.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { of } from 'rxjs';
import { UserService } from 'src/app/services/user.service';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LoginComponent],
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        MatSnackBarModule,
      ],
      providers: [
        {
          provide: UserService,
          useValue: {
            login: () => of({}),
            currentUserSubject: of({}),
          },
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('#allRequiredInputsValid() should return true, if password & email are correct', () => {
    component.passwordIsCorrect = true;
    component.emailIsCorrect = true;
    let result = component.allRequiredInputsValid();
    expect(component.loginButtonEnabled).toBeTrue();
    expect(result).toEqual(true);
  });

  it('#allRequiredInputsValid() should return false, if password or email is not correct', () => {
    component.passwordIsCorrect = false;
    component.emailIsCorrect = true;
    let result = component.allRequiredInputsValid();
    expect(result).toEqual(false);

    component.passwordIsCorrect = false;
    component.emailIsCorrect = false;
    result = component.allRequiredInputsValid();
    expect(result).toEqual(false);

    component.passwordIsCorrect = true;
    component.emailIsCorrect = false;
    result = component.allRequiredInputsValid();
    expect(result).toEqual(false);
  });

  it('#validateEmail() should validate correct email addresses', () => {
    component.email = 'test@testdev.com';
    expect(component.emailIsCorrect).toBeFalse();
    component.validateEmail();
    expect(component.emailIsCorrect).toBeTrue();
  });

  it('#validateEmail() should not validate incorrect email addresses', () => {
    component.email = 'test@@testdev.com';
    expect(component.emailIsCorrect).toBeFalse();
    component.validateEmail();
    expect(component.emailIsCorrect).toBeFalse();
  });

  it('#validatePassword() should validate correct passwords', () => {
    component.password = '123123123123';
    expect(component.passwordIsCorrect).toBeFalse();
    component.validatePassword();
    expect(component.passwordIsCorrect).toBeTrue();
  });

  it('#validatePassword() should not validate incorrect passwords (e.g. empty strings)', () => {
    component.password = '';
    expect(component.passwordIsCorrect).toBeFalse();
    component.validatePassword();
    expect(component.passwordIsCorrect).toBeFalse();
  });

  it('should call the correct route with the correct values when pressing the login button', fakeAsync(() => {
    // Arrange
    let loginButton =
      fixture.debugElement.nativeElement.querySelector('#loginButton');
    let emailInput =
      fixture.debugElement.nativeElement.querySelector('#emailInput');
    let passwordInput =
      fixture.debugElement.nativeElement.querySelector('#passwordInput');

    expect(loginButton).toBeTruthy();
    expect(emailInput).toBeTruthy();
    expect(passwordInput).toBeTruthy();

    // mock the ActivatedRoute object to provide a return url as query parameter
    const activatedRoute = TestBed.inject(ActivatedRoute);
    activatedRoute.queryParams = of({ returnUrl: '/test' });

    const router = TestBed.inject(Router);
    let spy = spyOn(router, 'navigate');

    let userService = TestBed.inject(UserService);
    let loginSpy = spyOn(userService, 'login' as never).and.returnValue(
      Promise.resolve() as never
    );

    // Act
    emailInput.value = 'email@test.com';
    emailInput.dispatchEvent(new Event('input'));
    passwordInput.value = 'Test333!';
    passwordInput.dispatchEvent(new Event('input'));

    fixture.detectChanges();

    loginButton.click();

    // Assert
    fixture.whenStable().then(() => {
      expect(spy).toHaveBeenCalledWith(['/test']);
    })
  }));
});
