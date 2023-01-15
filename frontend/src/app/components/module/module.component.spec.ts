import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ModuleComponent } from './module.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {MatDialogModule} from "@angular/material/dialog";
import {DebugElement} from "@angular/core";
describe('ModuleComponent', () => {
  let component: ModuleComponent;
  let fixture: ComponentFixture<ModuleComponent>;
  let de: DebugElement;

  beforeEach(async () => {
     await TestBed.configureTestingModule({
      declarations: [ModuleComponent],
      imports: [RouterTestingModule, HttpClientTestingModule, MatSnackBarModule, MatDialogModule]
    })
      .compileComponents();
  });

  beforeEach( () => {
    fixture = TestBed.createComponent(ModuleComponent);
    component = fixture.componentInstance;
    de = fixture.debugElement;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it ('should not show loading after init', () => {
    expect(component.showLoading).toBeFalse();
  });

});
