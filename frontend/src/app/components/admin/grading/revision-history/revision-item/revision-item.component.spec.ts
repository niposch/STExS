import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevisionItemComponent } from './revision-item.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('RevisionItemComponent', () => {
  let component: RevisionItemComponent;
  let fixture: ComponentFixture<RevisionItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevisionItemComponent ],
      imports: [HttpClientTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RevisionItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
