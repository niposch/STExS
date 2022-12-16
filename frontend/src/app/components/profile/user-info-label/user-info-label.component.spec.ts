import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserInfoLabelComponent } from './user-info-label.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";

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
});
