import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditClozeComponent } from './create-edit-cloze.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('CreateEditClozeComponent', () => {
  let component: CreateEditClozeComponent;
  let fixture: ComponentFixture<CreateEditClozeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEditClozeComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule, MatSnackBarModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEditClozeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
