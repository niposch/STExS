import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradingExerciseDashboardComponent } from './grading-exercise-dashboard.component';

describe('GradingExerciseDashboardComponent', () => {
  let component: GradingExerciseDashboardComponent;
  let fixture: ComponentFixture<GradingExerciseDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradingExerciseDashboardComponent ]
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
