import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModulesUserComponent } from './modules-user.component';

describe('ModulesUserComponent', () => {
  let component: ModulesUserComponent;
  let fixture: ComponentFixture<ModulesUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ModulesUserComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModulesUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
