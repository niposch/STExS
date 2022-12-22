import {ComponentFixture, fakeAsync, TestBed, tick} from '@angular/core/testing';
import { UserInfoLabelComponent } from './user-info-label.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";
import {EventEmitter} from "@angular/core";
import {By} from "@angular/platform-browser";

describe('UserInfoLabelComponent', () => {
  let component: UserInfoLabelComponent;
  let fixture: ComponentFixture<UserInfoLabelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserInfoLabelComponent ],
      imports: [HttpClientTestingModule, RouterTestingModule]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UserInfoLabelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
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

  it ('#startEditing() should toggle #isEditing',() => {
    expect(component.isEditing).toBeFalse();
    component.startEditing();
    expect(component.isEditing).toBeTrue();
  });

  it('edit Button should call #startEditing()', fakeAsync(() => {
    spyOn(component, 'startEditing');
    let button = fixture.debugElement.nativeElement.querySelector('app-edit-button');
    button.click();
    tick();
    expect(component.startEditing).toHaveBeenCalled();
  }));

  it('edit Button while editing should call #editButtonClick()', fakeAsync(() => {
    component.startEditing();
    spyOn(component, 'editButtonClick');
    let button = fixture.debugElement.nativeElement.querySelector('app-edit-button');
    expect(component.isEditing).toBeTrue();
    button.click();
    tick();
    expect(component.editButtonClick).toHaveBeenCalled();
  }));

  /*
  // TODO TEST EMAIL VALIDATION

  it ('#validateEmail() should validate emails correctly',() => {
    component.startEditing();
    let input = fixture.debugElement.nativeElement.querySelector('input');

    input.value = "dev@devtest.com"; //email is correct
    input.dispatchEvent(new Event('keyup'));

    expect(component.emailIsCorrect).toBeTrue(); //error: component.emailIsCorrect is false
  });
  */
});
