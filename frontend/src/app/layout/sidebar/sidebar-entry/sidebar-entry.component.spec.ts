import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SidebarEntryComponent } from './sidebar-entry.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {MatMenuModule} from "@angular/material/menu";
import {CdkAccordionModule} from "@angular/cdk/accordion";

describe('SidebarEntryComponent', () => {
  let component: SidebarEntryComponent;
  let fixture: ComponentFixture<SidebarEntryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SidebarEntryComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule, CdkAccordionModule, MatMenuModule]
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
