import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChapterAdminAdministrateComponent } from './chapter-admin-administrate.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";

describe('ChapterAdminAdministrateComponent', () => {
  let component: ChapterAdminAdministrateComponent;
  let fixture: ComponentFixture<ChapterAdminAdministrateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChapterAdminAdministrateComponent ],
      imports: [RouterTestingModule, HttpClientTestingModule]
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
