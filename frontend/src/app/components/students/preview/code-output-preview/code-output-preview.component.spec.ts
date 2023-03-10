import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodeOutputPreviewComponent } from './code-output-preview.component';

describe('CodeOutputPreviewComponent', () => {
  let component: CodeOutputPreviewComponent;
  let fixture: ComponentFixture<CodeOutputPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CodeOutputPreviewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CodeOutputPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
