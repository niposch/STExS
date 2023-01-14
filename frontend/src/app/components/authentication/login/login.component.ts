import {ChangeDetectorRef, Component, OnInit, ViewChild} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {UserService} from "../../../services/user.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {of} from "rxjs";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @ViewChild('passwordInput') passwordInputField: any;

  public showLoading: boolean = false;

  public showEmailError: boolean = false;
  public showPasswordError: boolean = false;

  public emailIsCorrect: boolean = false;
  public passwordIsCorrect: boolean = false;

  public showPassword: boolean = false;

  public loginButtonEnabled: boolean = false;
  public password: string = "";
  public email: string = "";
  private callbackUrl = "/dashboard";

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private readonly snackbarService:MatSnackBar,
    private readonly changeDetectorRef: ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      let callbackUrl = params['callbackUrl'];
      if (callbackUrl != null) {
        this.callbackUrl = callbackUrl;
      }
    });
    this.userService.currentUserSubject.subscribe(r =>{
      if(r != null){
        void this.router.navigate([this.callbackUrl]);
      }
    })
  }

  async login():Promise<void> {
    this.showLoading = true;
    await this.userService.login(this.email, this.password)
      .catch(err => {
        this.snackbarService.open("Login failed.", "Dismiss", {duration: 5000})
        this.showLoading = false;
        throw err
      })
      .then(() =>{
        this.showLoading = false;
      })
    await this.router.navigate([this.callbackUrl]);
  }

  validateEmail(event : Event | null = null) {
    // @ts-ignore
    const inputValue = event?.target?.value ?? this.email;

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

    this.allRequiredInputsValid();
  }

  validatePassword(event : Event | null = null) {
    // @ts-ignore
    const inputValue = event?.target?.value ?? this.password;

    if (inputValue == "") {
      this.showPasswordError = true;
      this.passwordIsCorrect = false;
    } else {
      this.showPasswordError = false;
      this.passwordIsCorrect = true;
    }

    this.allRequiredInputsValid();
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

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

}
