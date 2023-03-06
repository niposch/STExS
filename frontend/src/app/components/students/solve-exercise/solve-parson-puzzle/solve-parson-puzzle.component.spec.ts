import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolveParsonPuzzleComponent } from './solve-parson-puzzle.component';

describe('SolveParsonPuzzleComponent', () => {
  let component: SolveParsonPuzzleComponent;
  let fixture: ComponentFixture<SolveParsonPuzzleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolveParsonPuzzleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolveParsonPuzzleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
