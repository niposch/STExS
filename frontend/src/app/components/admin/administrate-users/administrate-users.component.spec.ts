import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrateUsersComponent } from './administrate-users.component';

describe('AdministrateUsersComponent', () => {
  let component: AdministrateUsersComponent;
  let fixture: ComponentFixture<AdministrateUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdministrateUsersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrateUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
