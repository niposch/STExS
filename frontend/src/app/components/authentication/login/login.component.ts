import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {UserService} from "../../../services/user.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public showLoading: boolean = false;
  public stayLoggedIn: boolean = false;

  public showEmailError: boolean = false;
  public showPasswordError: boolean = false;

  public emailIsCorrect: boolean = false;
  public passwordIsCorrect: boolean = false;

  public loginButtonEnabled: boolean = false;
  public password: string = "";
  public email: string = "";
  private callbackUrl = "/dashboard";

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      let callbackUrl = params['callbackUrl'];
      if (callbackUrl != null) {
        this.callbackUrl = callbackUrl;
      }
    });
  }

  login() {
    this.showLoading = true;
    this.userService.login(this.email, this.password).subscribe(
      (user) => {
        this.showLoading = false;
        console.log(user);
        this.router.navigate([this.callbackUrl],).then();
      })
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

    let allValid = this.allRequiredInputsValid()
    if (event.keyCode == 13 && allValid) {
      this.login();
      return;
    }
  }

  allRequiredInputsValid() {
    if (this.passwordIsCorrect && this.emailIsCorrect) {
      this.loginButtonEnabled = true;
      return true;
    } else {
      this.loginButtonEnabled = false;
    }
    return false;
  }
}
