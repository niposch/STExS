import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrateModuleComponent } from './administrate-module.component';
import {ActivatedRoute} from "@angular/router";
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatDialogModule} from "@angular/material/dialog";

describe('AdministrateModuleComponent', () => {
  let component: AdministrateModuleComponent;
  let fixture: ComponentFixture<AdministrateModuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministrateModuleComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrateModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
