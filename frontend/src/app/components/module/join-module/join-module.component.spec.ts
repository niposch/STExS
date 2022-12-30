import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinModuleComponent } from './join-module.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

describe('JoinModuleComponent', () => {
  let component: JoinModuleComponent;
  let fixture: ComponentFixture<JoinModuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JoinModuleComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, MatSnackBarModule ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JoinModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
