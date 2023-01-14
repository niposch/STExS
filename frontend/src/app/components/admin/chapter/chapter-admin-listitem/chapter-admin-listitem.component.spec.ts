import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChapterAdminListitemComponent } from './chapter-admin-listitem.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatDialogModule} from "@angular/material/dialog";

describe('ChapterAdminListitemComponent', () => {
  let component: ChapterAdminListitemComponent;
  let fixture: ComponentFixture<ChapterAdminListitemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChapterAdminListitemComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChapterAdminListitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
