import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChapterAdminListitemComponent } from './chapter-admin-listitem.component';

describe('ChapterAdminListitemComponent', () => {
  let component: ChapterAdminListitemComponent;
  let fixture: ComponentFixture<ChapterAdminListitemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChapterAdminListitemComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChapterAdminListitemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
