import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradingDialogComponent } from './grading-dialog.component';
import {MAT_DIALOG_DATA, MatDialogModule} from "@angular/material/dialog";
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('GradingDialogComponent', () => {
  let component: GradingDialogComponent;
  let fixture: ComponentFixture<GradingDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradingDialogComponent ],
      imports: [HttpClientTestingModule, MatDialogModule],
      providers: [
        {provide: MAT_DIALOG_DATA, useValue: {exerciseId: '1', userId: '1'}}
      ]
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
