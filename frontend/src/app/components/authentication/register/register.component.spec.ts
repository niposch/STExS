import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterComponent } from './register.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {compareNumbers} from "@angular/compiler-cli/src/version_helpers";

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it ('#allRequiredInputsValid() should return true, if password & email are correct', () => {
    component.emailIsCorrect = true;
    component.passwordIsCorrect = true;
    component.agreedToTOS = true;
    component.allRequiredInputsValid();
    expect(component.registerButtonEnabled).toEqual(true);
  });

  it ('#allRequiredInputsValid() should return false, if password or email is not correct', () => {
    component.emailIsCorrect = false;
    component.passwordIsCorrect = true;
    component.agreedToTOS = true;
    component.allRequiredInputsValid();
    expect(component.registerButtonEnabled).toEqual(false);
  });

  it ('#validateEmail() should validate correct email addresses', () => {
    component.email = "test@testdev.com";
    expect(component.emailIsCorrect).toBeFalse();
    component.validateEmail();
    expect(component.emailIsCorrect).toBeTrue();
  });

  it ('#validateEmail() should not validate incorrect email addresses', () => {
    component.email = "test@@testdev.com";
    expect(component.emailIsCorrect).toBeFalse();
    component.validateEmail();
    expect(component.emailIsCorrect).toBeFalse();
  });

  it ('#validatePassword() should validate correct passwords', () => {
    component.password = "ABCabc!12";
    expect(component.passwordIsCorrect).toBeFalse();
    component.validatePassword();
    expect(component.passwordIsCorrect).toBeTrue();
  });

  it ('#validatePassword() should not validate incorrect passwords (e.g. empty strings)', () => {
    component.password = "asd";
    expect(component.passwordIsCorrect).toBeFalse();
    component.validatePassword();
    expect(component.passwordIsCorrect).toBeFalse();
  });
});
