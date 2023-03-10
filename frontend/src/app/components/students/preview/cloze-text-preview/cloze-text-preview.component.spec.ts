import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClozeTextPreviewComponent } from './cloze-text-preview.component';

describe('ClozeTextPreviewComponent', () => {
  let component: ClozeTextPreviewComponent;
  let fixture: ComponentFixture<ClozeTextPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClozeTextPreviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ClozeTextPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
