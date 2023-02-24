import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeRoleDialogComponent } from './change-role-dialog.component';
import {MatDialogModule, MatDialogRef, MAT_DIALOG_DATA} from "@angular/material/dialog";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('ChangeRoleDialogComponent', () => {
  let component: ChangeRoleDialogComponent;
  let fixture: ComponentFixture<ChangeRoleDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChangeRoleDialogComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule],
      providers: [
        { provide: MAT_DIALOG_DATA, useValue: {user: {firstName: "testName"}} },
        {provide: MatDialogRef, useValue: {data: {}}},
      ],
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChangeRoleDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
