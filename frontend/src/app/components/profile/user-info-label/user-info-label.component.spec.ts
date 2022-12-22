import {ComponentFixture, fakeAsync, TestBed, tick} from '@angular/core/testing';
import { UserInfoLabelComponent } from './user-info-label.component';
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {RouterTestingModule} from "@angular/router/testing";

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

  it ('edit button should activate edit mode', fakeAsync(() => {
    spyOn(component, 'startEditing');
    let button = fixture.debugElement.nativeElement.querySelectorAll('app-edit-button')[0];
    button.click();
    tick();
    expect(component.startEditing).toHaveBeenCalled();
  }));
});
