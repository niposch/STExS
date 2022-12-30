import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleCreateComponent } from './module-create.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('ModuleCreateComponent', () => {
  let component: ModuleCreateComponent;
  let fixture: ComponentFixture<ModuleCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModuleCreateComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, MatSnackBarModule ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModuleCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
