import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradingModuleDashboardComponent } from './grading-module-dashboard.component';
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
describe('GradingModuleDashboardComponent', () => {
  let component: GradingModuleDashboardComponent;
  let fixture: ComponentFixture<GradingModuleDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradingModuleDashboardComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule],
    })
    .compileComponents();

    fixture = TestBed.createComponent(GradingModuleDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
