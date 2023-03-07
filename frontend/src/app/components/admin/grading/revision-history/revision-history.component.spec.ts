import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevisionHistoryComponent } from './revision-history.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MAT_DIALOG_DATA, MatDialogModule} from "@angular/material/dialog";

describe('RevisionHistoryComponent', () => {
  let component: RevisionHistoryComponent;
  let fixture: ComponentFixture<RevisionHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevisionHistoryComponent ],
      imports: [HttpClientTestingModule, MatDialogModule],
      providers: [
        {provide: MAT_DIALOG_DATA, useValue: {exerciseId: '1', userId: '1'}}
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RevisionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
