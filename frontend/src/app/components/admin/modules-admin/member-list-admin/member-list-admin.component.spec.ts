import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberListAdminComponent } from './member-list-admin.component';

describe('MemberListAdminComponent', () => {
  let component: MemberListAdminComponent;
  let fixture: ComponentFixture<MemberListAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MemberListAdminComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MemberListAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
