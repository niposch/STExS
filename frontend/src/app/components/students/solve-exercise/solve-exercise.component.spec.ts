import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolveExerciseComponent } from './solve-exercise.component';

describe('SolveExerciseComponent', () => {
  let component: SolveExerciseComponent;
  let fixture: ComponentFixture<SolveExerciseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolveExerciseComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolveExerciseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
