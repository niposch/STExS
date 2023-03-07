import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradingDialogComponent } from './grading-dialog.component';

describe('GradingDialogComponent', () => {
  let component: GradingDialogComponent;
  let fixture: ComponentFixture<GradingDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradingDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GradingDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
