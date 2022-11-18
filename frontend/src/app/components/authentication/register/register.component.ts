import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public showLoading:boolean = false;
  
  //show errors
  public showEmailError:boolean = false;
  public showPasswordError:boolean = false;

  //enable / disable register button
  public agreedToTOS: boolean = false;
  private emailIsCorrect:boolean = false;
  private passwordIsCorrect:boolean = false;

  public registerButtonEnabled:boolean = false;

  //seperated RegEx tests to give user password feedback
  public PW1Uppercase:boolean = false;
  public PW1Lowercase:boolean = false;
  public PW1SpecialChar:boolean = false;
  public PW1Number:boolean = false;
  public PW8CharsLong:boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  register() {
    this.showLoading = true;

    setTimeout(() =>{
      this.showLoading = false;
    }, 2000)
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

    this.allRequiredInputsValid()
  }

  validatePassword(event: any) {
    const inputValue = event.target.value;

    //RegEx for all the password conditions
    let Reg1UppercaseLetter = new RegExp(/^(?=.*?[A-Z]).{1,}$/);
    let Reg1LowercaseLetter = new RegExp(/^(?=.*?[a-z]).{1,}$/);
    let Reg1Number = new RegExp(/^(?=.*?[0-9]).{1,}$/);
    let Reg1SpecialChar = new RegExp(/^(?=.*?[#?!@$%^&*-]).{1,}$/);
    let Reg8CharsLong = new RegExp(/^.{8,}$/);

    //tests for every password condition
    this.PW1Uppercase = Reg1UppercaseLetter.test(inputValue);
    this.PW1Lowercase = Reg1LowercaseLetter.test(inputValue);
    this.PW1Number = Reg1Number.test(inputValue);
    this.PW1SpecialChar = Reg1SpecialChar.test(inputValue);
    this.PW8CharsLong = Reg8CharsLong.test(inputValue);

    //all conditions satisfied?
    let PWIsValid = this.PW1Uppercase && this.PW1Lowercase && this.PW1Number && this. PW1SpecialChar && this.PW8CharsLong;

    if (inputValue == "" || !PWIsValid) {
      this.showPasswordError = true;
      this.passwordIsCorrect = false;
    } else {
      this.showPasswordError = false;
      this.passwordIsCorrect = true;
    }

    this.allRequiredInputsValid()
  }

  allRequiredInputsValid() {
    if (this.emailIsCorrect && this.passwordIsCorrect && this.agreedToTOS) {
      this.registerButtonEnabled = true;
    } else {
      this.registerButtonEnabled = false;
    }
  }
}
