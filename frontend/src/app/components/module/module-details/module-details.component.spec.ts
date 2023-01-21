import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModuleDetailsComponent } from './module-details.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";

describe('ModuleDetailsComponent', () => {
  let component: ModuleDetailsComponent;
  let fixture: ComponentFixture<ModuleDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModuleDetailsComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, MatSnackBarModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModuleDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
