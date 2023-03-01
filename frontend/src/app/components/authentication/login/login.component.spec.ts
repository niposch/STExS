import {
  ComponentFixture,
  fakeAsync,
  TestBed,
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

  it('#validateEmail() should validate correct email addresses', () => {
    component.email = 'test@testdev.com';
    expect(component.showEmailError).toBeFalse();
    component.validateEmail();
    expect(component.showEmailError).toBeFalse();
  });

  it('#validateEmail() should not validate incorrect email addresses', () => {
    component.email = 'test@@testdev.com';
    expect(component.showEmailError).toBeFalse();
    component.validateEmail();
    expect(component.showEmailError).toBeTrue();
  });

  it('#validatePassword() should validate correct passwords', () => {
    component.password = '123123123123';
    expect(component.showPasswordError).toBeFalse();
    component.isInputCorrect();
    expect(component.showPasswordError).toBeFalse();
  });

  it('#validatePassword() should not validate incorrect passwords (e.g. empty strings)', () => {
    component.password = '';
    expect(component.showPasswordError).toBeFalse();
    component.isInputCorrect();
    expect(component.showPasswordError).toBeTrue();
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
    emailInput.value = 'dev@test.com';
    emailInput.dispatchEvent(new Event('input'));
    passwordInput.value = 'Test333!';
    passwordInput.dispatchEvent(new Event('input'));

    fixture.detectChanges();

    component.loginProcedure();
    // Assert
    fixture.whenStable().then(() => {
      expect(spy).toHaveBeenCalledWith(['/test']);
    })
  }));
});
