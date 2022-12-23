import { ComponentFixture, TestBed } from '@angular/core/testing';

import { JoinModuleComponent } from './join-module.component';

describe('JoinModuleComponent', () => {
  let component: JoinModuleComponent;
  let fixture: ComponentFixture<JoinModuleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ JoinModuleComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(JoinModuleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
