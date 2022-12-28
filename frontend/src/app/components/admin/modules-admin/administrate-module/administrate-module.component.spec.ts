import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrateModuleComponent } from './administrate-module.component';

describe('AdministrateModuleComponent', () => {
  let component: AdministrateModuleComponent;
  let fixture: ComponentFixture<AdministrateModuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministrateModuleComponent ]
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
