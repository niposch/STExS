import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it ('#allRequiredInputsValid() should return true, if password & email are correct', () => {
    component.passwordIsCorrect = true;
    component.emailIsCorrect = true;
    let result = component.allRequiredInputsValid();
    expect(component.loginButtonEnabled).toBeTrue();
    expect(result).toEqual(true);
  });

  it ('#allRequiredInputsValid() should return false, if password or email is not correct', () => {
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

  it ('#validateEmail() should validate "test@testdev.com"', () => {
    component.email = "test@testdev.com";
    expect(component.emailIsCorrect).toBeFalse();
    component.validateEmail();
    expect(component.emailIsCorrect).toBeTrue();
  });

  it ('#validateEmail() should not validate "test@@testdev.com"', () => {
    component.email = "test@@testdev.com";
    expect(component.emailIsCorrect).toBeFalse();
    component.validateEmail();
    expect(component.emailIsCorrect).toBeFalse();
  });

  it ('#validatePassword() should validate "123123123123"', () => {
    component.password = "123123123123";
    expect(component.passwordIsCorrect).toBeFalse();
    component.validatePassword();
    expect(component.passwordIsCorrect).toBeTrue();
  });

  it ('#validatePassword() should not validate ""', () => {
    component.password = "";
    expect(component.passwordIsCorrect).toBeFalse();
    component.validatePassword();
    expect(component.passwordIsCorrect).toBeFalse();
  });
});
