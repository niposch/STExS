import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditClozeComponent } from './create-edit-cloze.component';

describe('CreateEditClozeComponent', () => {
  let component: CreateEditClozeComponent;
  let fixture: ComponentFixture<CreateEditClozeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEditClozeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEditClozeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
