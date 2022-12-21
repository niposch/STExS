import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserInfoLabelComponent } from './user-info-label.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {ProfileComponent} from "../profile.component";
import {By} from "@angular/platform-browser";

describe('UserInfoLabelComponent', () => {
  let component: UserInfoLabelComponent;
  let fixture: ComponentFixture<UserInfoLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserInfoLabelComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserInfoLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it ('#editButtonClick() should toggle #isEditing', () => {
    // @ts-ignore
    const comp = new UserInfoLabelComponent();
    expect(comp.isEditing)
      .withContext('not editing at first')
      .toBe(false)
    comp.editButtonClick();
    expect(comp.isEditing)
      .withContext('editing after click')
      .toBe(true);
    comp.editButtonClick();
    expect(comp.isEditing)
      .withContext('not editing after second click')
      .toBe(false);
  });

/*
  // Figure out how to trigger Events
  it ('#validateEmail() should validate "test@testdev.com" correctly', ()=>{
    // @ts-ignore
    const component = new UserInfoLabelComponent();

    expect(component.showEmailError).toBe(true);
    expect(component.emailIsCorrect).toBe(false);
    component.validateEmail({target:{value:"test@testdev.com"}});
    expect(component.showEmailError).toBe(false);
    expect(component.emailIsCorrect).toBe(true);
  });

 */
});
