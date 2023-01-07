import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChapterAdminListComponent } from './chapter-admin-list.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('ChapterAdminListComponent', () => {
  let component: ChapterAdminListComponent;
  let fixture: ComponentFixture<ChapterAdminListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChapterAdminListComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, MatSnackBarModule ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChapterAdminListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
