import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndentedDropListComponent } from './indented-drop-list.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {CdkDropList, DragDropModule} from "@angular/cdk/drag-drop";

describe('IndentedDropListComponent', () => {
  let component: IndentedDropListComponent;
  let fixture: ComponentFixture<IndentedDropListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndentedDropListComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule, DragDropModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndentedDropListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
