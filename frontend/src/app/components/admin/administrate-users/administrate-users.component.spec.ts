import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrateUsersComponent } from './administrate-users.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatDialogModule} from "@angular/material/dialog";

describe('AdministrateUsersComponent', () => {
  let component: AdministrateUsersComponent;
  let fixture: ComponentFixture<AdministrateUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministrateUsersComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrateUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
