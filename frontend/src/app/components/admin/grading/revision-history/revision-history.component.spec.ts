import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RevisionHistoryComponent } from './revision-history.component';

describe('RevisionHistoryComponent', () => {
  let component: RevisionHistoryComponent;
  let fixture: ComponentFixture<RevisionHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RevisionHistoryComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RevisionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
