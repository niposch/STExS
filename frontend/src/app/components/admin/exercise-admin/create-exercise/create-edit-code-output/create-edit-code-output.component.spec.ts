import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditCodeOutputComponent } from './create-edit-code-output.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('CreateEditCodeOutputComponent', () => {
  let component: CreateEditCodeOutputComponent;
  let fixture: ComponentFixture<CreateEditCodeOutputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEditCodeOutputComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEditCodeOutputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
