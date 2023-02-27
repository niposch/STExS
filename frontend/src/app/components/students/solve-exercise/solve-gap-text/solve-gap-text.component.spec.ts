import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolveGapTextComponent } from './solve-gap-text.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('SolveGapTextComponent', () => {
  let component: SolveGapTextComponent;
  let fixture: ComponentFixture<SolveGapTextComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolveGapTextComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule, MatSnackBarModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolveGapTextComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
