import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolveParsonPuzzleComponent } from './solve-parson-puzzle.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('SolveParsonPuzzleComponent', () => {
  let component: SolveParsonPuzzleComponent;
  let fixture: ComponentFixture<SolveParsonPuzzleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolveParsonPuzzleComponent ],
      imports: [HttpClientTestingModule, MatSnackBarModule]
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
