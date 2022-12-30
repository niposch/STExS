import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChapterAdminListComponent } from './chapter-admin-list.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";

describe('ChapterAdminListComponent', () => {
  let component: ChapterAdminListComponent;
  let fixture: ComponentFixture<ChapterAdminListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChapterAdminListComponent ],
      imports: [HttpClientTestingModule]
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
