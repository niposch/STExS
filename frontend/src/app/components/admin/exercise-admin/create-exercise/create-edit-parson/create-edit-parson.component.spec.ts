import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditParsonComponent } from './create-edit-parson.component';
import {DragDropModule} from "@angular/cdk/drag-drop";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('CreateEditParsonComponent', () => {
  let component: CreateEditParsonComponent;
  let fixture: ComponentFixture<CreateEditParsonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEditParsonComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, DragDropModule, MatSnackBarModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEditParsonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
