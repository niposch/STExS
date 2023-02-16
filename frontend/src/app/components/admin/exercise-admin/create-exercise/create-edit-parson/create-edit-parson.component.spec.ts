import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditParsonComponent } from './create-edit-parson.component';

describe('CreateEditParsonComponent', () => {
  let component: CreateEditParsonComponent;
  let fixture: ComponentFixture<CreateEditParsonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEditParsonComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEditParsonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
