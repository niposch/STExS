import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberListAdminComponent } from './member-list-admin.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('MemberListAdminComponent', () => {
  let component: MemberListAdminComponent;
  let fixture: ComponentFixture<MemberListAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MemberListAdminComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule]

    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberListAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
