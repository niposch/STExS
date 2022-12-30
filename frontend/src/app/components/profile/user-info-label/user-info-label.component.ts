import {ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';

@Component({
  selector: 'app-user-info-label',
  templateUrl: './user-info-label.component.html',
  styleUrls: ['./user-info-label.component.scss']
})
export class UserInfoLabelComponent implements OnInit {
  @Input() attributeName: string = "null";
  @Input() attributeType: "text" | "email" = "text";
  @Input() attributeInfo: string = "";
  @Input() value: string | null | undefined = "" // two way data binding in angular
  @Output() valueChange: EventEmitter<string> = new EventEmitter<string>();

  @ViewChild('inputElement', {static: false}) inputElementRef!: ElementRef;
  public isEditing: boolean = false;
  public showEmailError = true;
  public emailIsCorrect = false;

  constructor(private readonly changeDetector: ChangeDetectorRef) {
  }

  ngOnInit(): void {
  }

  editButtonClick() {
    this.isEditing = !this.isEditing;
    //for passing value to parent object
    // @ts-ignore
    this.valueChange.emit(this.value);
  }

  validateEmail() {
    if (this.attributeType != "email") {
      return;
    }
    const inputValue = this.value;

    //RegEx for emails
    let regex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
    // @ts-ignore
    let emailCorrect = regex.test(inputValue);

    if (inputValue == "" || !emailCorrect) {
      this.showEmailError = true;
      this.emailIsCorrect = false;
    } else {
      this.showEmailError = false;
      this.emailIsCorrect = true;
    }
  }

  startEditing() {
    this.isEditing = !this.isEditing
    this.changeDetector.detectChanges();
    this.validateEmail();
    this.inputElementRef.nativeElement.focus()
    this.inputElementRef.nativeElement.select()
  }
}
