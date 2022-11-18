import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public showLoading:boolean = false;
  public stayLoggedIn:boolean = false;

  public showEmailError:boolean = false;
  public showPasswordError:boolean = false;

  public emailIsCorrect:boolean = false;
  public passwordIsCorrect:boolean = false;

  public loginButtonEnabled:boolean = false;
  
  constructor(
    private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      let callbackUrl = params['callbackUrl'];
      if (callbackUrl == null) {
        callbackUrl = '/';
      }
      console.log(callbackUrl);
    });
  }

  login(){
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

    if (inputValue == "") {
      this.showPasswordError = true;
      this.passwordIsCorrect = false;
    } else {
      this.showPasswordError = false;
      this.passwordIsCorrect = true;
    }

    this.allRequiredInputsValid()
  }

  allRequiredInputsValid() {
    if (this.passwordIsCorrect && this.emailIsCorrect) {
      this.loginButtonEnabled = true;
    } else {
      this.loginButtonEnabled = false;
    }
  }
}