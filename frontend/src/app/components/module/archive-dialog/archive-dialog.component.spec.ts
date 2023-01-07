import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ArchiveDialogComponent } from './archive-dialog.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatDialogRef} from "@angular/material/dialog";

describe('ArchiveDialogComponent', () => {
  let component: ArchiveDialogComponent;
  let fixture: ComponentFixture<ArchiveDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ArchiveDialogComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule],
      providers: [
        { provide: MatDialogRef, useValue: {} }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ArchiveDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
