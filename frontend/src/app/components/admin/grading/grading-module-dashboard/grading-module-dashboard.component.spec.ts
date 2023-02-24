import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GradingModuleDashboardComponent } from './grading-module-dashboard.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('GradingModuleDashboardComponent', () => {
  let component: GradingModuleDashboardComponent;
  let fixture: ComponentFixture<GradingModuleDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GradingModuleDashboardComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule]
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
