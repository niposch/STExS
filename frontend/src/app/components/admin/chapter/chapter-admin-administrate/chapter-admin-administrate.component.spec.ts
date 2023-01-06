import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChapterAdminAdministrateComponent } from './chapter-admin-administrate.component';

describe('ChapterAdminAdministrateComponent', () => {
  let component: ChapterAdminAdministrateComponent;
  let fixture: ComponentFixture<ChapterAdminAdministrateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChapterAdminAdministrateComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChapterAdminAdministrateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
