import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolveCodeOutputComponent } from './solve-code-output.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatDialogModule} from "@angular/material/dialog";

describe('SolveCodeOutputComponent', () => {
  let component: SolveCodeOutputComponent;
  let fixture: ComponentFixture<SolveCodeOutputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolveCodeOutputComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolveCodeOutputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
