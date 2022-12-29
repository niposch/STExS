import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChapterAdminListComponent } from './chapter-admin-list.component';

describe('ChapterAdminListComponent', () => {
  let component: ChapterAdminListComponent;
  let fixture: ComponentFixture<ChapterAdminListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChapterAdminListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChapterAdminListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
