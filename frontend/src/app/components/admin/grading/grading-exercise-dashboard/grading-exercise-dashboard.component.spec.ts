import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradingExerciseDashboardComponent } from './grading-exercise-dashboard.component';
import {MatDialogModule} from "@angular/material/dialog";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";

describe('GradingExerciseDashboardComponent', () => {
  let component: GradingExerciseDashboardComponent;
  let fixture: ComponentFixture<GradingExerciseDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradingExerciseDashboardComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GradingExerciseDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
