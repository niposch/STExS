import {ComponentFixture, fakeAsync, TestBed, tick} from '@angular/core/testing';
import {DeleteDialogComponent} from './delete-dialog.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatDialogRef} from "@angular/material/dialog";

describe('DeleteDialogComponent', () => {
  let component: DeleteDialogComponent;
  let fixture: ComponentFixture<DeleteDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteDialogComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule],
      providers: [
        { provide: MatDialogRef, useValue: {} }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  fit('should close dialog when close button clicked', fakeAsync(() => {
    let abortButton = fixture.debugElement.query(By.css("#abortButton"))
    expect(abortButton).toBeTruthy();
    abortButton.triggerEventHandler("click");
    fixture.whenStable().then(() => {
    })
  }));
});
