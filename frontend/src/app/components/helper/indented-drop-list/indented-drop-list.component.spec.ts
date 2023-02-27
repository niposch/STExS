import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IndentedDropListComponent } from './indented-drop-list.component';

describe('IndentedDropListComponent', () => {
  let component: IndentedDropListComponent;
  let fixture: ComponentFixture<IndentedDropListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IndentedDropListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(IndentedDropListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
