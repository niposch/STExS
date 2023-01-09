import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SidebarEntryComponent } from './sidebar-entry.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";

describe('SidebarEntryComponent', () => {
  let component: SidebarEntryComponent;
  let fixture: ComponentFixture<SidebarEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SidebarEntryComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SidebarEntryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
