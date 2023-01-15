import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditCodeOutputComponent } from './create-edit-code-output.component';

describe('CreateEditCodeOutputComponent', () => {
  let component: CreateEditCodeOutputComponent;
  let fixture: ComponentFixture<CreateEditCodeOutputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateEditCodeOutputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateEditCodeOutputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
