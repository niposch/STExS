import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfileComponent } from './profile.component';
import { HttpClientTestingModule } from "@angular/common/http/testing";
import { RouterTestingModule}  from "@angular/router/testing";
import {UserService} from "../../services/user.service";
import {AuthenticateService} from "../../../services/generated/services/authenticate.service";
import {ApiConfiguration} from "../../../services/generated/api-configuration";
import {HttpClient, HttpHandler} from "@angular/common/http";

describe('ProfileComponent', () => {
  let component: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfileComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule],
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it ('#editButtonClick() should toggle #isEditingName', () => {
    expect(component.isEditingName)
      .withContext('not editing at first')
      .toBe(false)
    component.editButtonClick();
    expect(component.isEditingName)
      .withContext('editing after click')
      .toBe(true);
    component.editButtonClick();
    expect(component.isEditingName)
      .withContext('not editing after second click')
      .toBe(false);
  });

  it('#editButtonClick() should set #userName to #newUserName if done editing', () => {
    component.userName = "example";
    component.newUserName = "other name";
    component.isEditingName = true;
    expect(component.userName == component.newUserName)
      .withContext('not equal at first')
      .toBe(false);
    component.editButtonClick();
    expect(component.userName == component.newUserName)
      .withContext('after #editButtonClick()')
      .toBe(true);
  });
});
