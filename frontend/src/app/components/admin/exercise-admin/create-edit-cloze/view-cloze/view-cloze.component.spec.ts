import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewClozeComponent } from './view-cloze.component';

describe('ViewClozeComponent', () => {
  let component: ViewClozeComponent;
  let fixture: ComponentFixture<ViewClozeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewClozeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewClozeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
