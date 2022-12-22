import {ComponentFixture, fakeAsync, TestBed, tick, waitForAsync} from '@angular/core/testing';

import { UserInfoLabelComponent } from './user-info-label.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {ProfileComponent} from "../profile.component";
import {By} from "@angular/platform-browser";
import {EventEmitter} from "@angular/core";

describe('UserInfoLabelComponent', () => {
  let fixture: ComponentFixture<UserInfoLabelComponent>;
  let component: UserInfoLabelComponent;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ HttpClientTestingModule, RouterTestingModule ],
      declarations: [ UserInfoLabelComponent ],
      providers: []
    })
    .compileComponents().then(() => {
        fixture = TestBed.createComponent(UserInfoLabelComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it ('#editButtonClick() should toggle #isEditing', () => {
    expect(component.isEditing)
      .withContext('not editing at first')
      .toBe(false)
    component.editButtonClick();
    expect(component.isEditing)
      .withContext('editing after click')
      .toBe(true);
    component.editButtonClick();
    expect(component.isEditing)
      .withContext('not editing after second click')
      .toBe(false);
  });

  it ('edit button should activate edit mode', fakeAsync(() => {
    spyOn(component, 'startEditing');
    let button = fixture.debugElement.nativeElement.querySelectorAll('app-edit-button')[0];
    button.click();
    tick();
    expect(component.startEditing).toHaveBeenCalled();
  }));
});
