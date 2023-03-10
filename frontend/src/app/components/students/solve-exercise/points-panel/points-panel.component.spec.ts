import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PointsPanelComponent } from './points-panel.component';

describe('PointsPanelComponent', () => {
  let component: PointsPanelComponent;
  let fixture: ComponentFixture<PointsPanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PointsPanelComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PointsPanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
