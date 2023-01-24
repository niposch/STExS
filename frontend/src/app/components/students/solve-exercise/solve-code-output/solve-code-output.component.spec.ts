import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolveCodeOutputComponent } from './solve-code-output.component';

describe('SolveCodeOutputComponent', () => {
  let component: SolveCodeOutputComponent;
  let fixture: ComponentFixture<SolveCodeOutputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SolveCodeOutputComponent ]
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
