import {ComponentFixture, fakeAsync, TestBed, tick} from '@angular/core/testing';
import {DeleteDialogComponent} from './delete-dialog.component';
import {RouterTestingModule} from "@angular/router/testing";
import {HttpClientTestingModule} from "@angular/common/http/testing";
import {MatDialogRef} from "@angular/material/dialog";
import {By} from "@angular/platform-browser";

describe('DeleteDialogComponent', () => {
  let component: DeleteDialogComponent;
  let fixture: ComponentFixture<DeleteDialogComponent>;
  let dialogRef = {
    close: (action:string) => {}
  }

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeleteDialogComponent ],
      imports: [ RouterTestingModule, HttpClientTestingModule],
      providers: [
        { useValue: dialogRef, provide: MatDialogRef<DeleteDialogComponent> }
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

  it('should close dialog when close button clicked', fakeAsync(() => {
    let abortButton = fixture.debugElement.query(By.css("#abortButton"))
    expect(abortButton).toBeTruthy();

    let spy = spyOn(dialogRef, "close")

    abortButton.triggerEventHandler("click");
    fixture.whenStable().then(() => {
      expect(spy).toHaveBeenCalledWith("abort")
    })
  }));
});
