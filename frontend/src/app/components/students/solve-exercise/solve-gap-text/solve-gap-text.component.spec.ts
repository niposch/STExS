import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolveGapTextComponent } from './solve-gap-text.component';

describe('SolveGapTextComponent', () => {
  let component: SolveGapTextComponent;
  let fixture: ComponentFixture<SolveGapTextComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolveGapTextComponent ]
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
