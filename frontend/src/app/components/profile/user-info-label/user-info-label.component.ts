import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-user-info-label',
  templateUrl: './user-info-label.component.html',
  styleUrls: ['./user-info-label.component.scss']
})
export class UserInfoLabelComponent {
  @Input() attributeName: string = "null";
  @Input() attributeInfo: string = "null";

  isEditing : boolean = false;

  newValue : string = "";

  showEmailError = true;
  emailIsCorrect = false;

  constructor() {}


  editButtonClick() {
    this.isEditing = !this.isEditing;
    this.attributeInfo = this.newValue;

    console.log(this.newValue);
    //set new value for user attribute
  }

  validateEmail(event: any) {
    const inputValue = event.target.value;

    //RegEx for emails
    let regex = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);
    let emailIsCorrect = regex.test(inputValue);

    if (inputValue == "" || !emailIsCorrect) {
      this.showEmailError = true;
      this.emailIsCorrect = false;
    } else {
      this.showEmailError = false;
      this.emailIsCorrect = true;
    }
  }
}
