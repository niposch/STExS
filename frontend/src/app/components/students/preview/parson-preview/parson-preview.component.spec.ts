import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ParsonPreviewComponent } from './parson-preview.component';

describe('ParsonPreviewComponent', () => {
  let component: ParsonPreviewComponent;
  let fixture: ComponentFixture<ParsonPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ParsonPreviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParsonPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
