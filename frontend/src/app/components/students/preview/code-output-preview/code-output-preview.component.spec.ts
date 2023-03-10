import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CodeOutputPreviewComponent } from './code-output-preview.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatDialogModule} from "@angular/material/dialog";

describe('CodeOutputPreviewComponent', () => {
  let component: CodeOutputPreviewComponent;
  let fixture: ComponentFixture<CodeOutputPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CodeOutputPreviewComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule]
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
