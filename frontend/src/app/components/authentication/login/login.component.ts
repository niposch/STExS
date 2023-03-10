import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from '../../../services/user.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { lastValueFrom } from 'rxjs';
import { AuthenticateService } from '../../../../services/generated/services/authenticate.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  @ViewChild('passwordInput') passwordInputField: any;

  public showLoading: boolean = false;

  public showEmailError: boolean = false;
  public showPasswordError: boolean = false;
  public showPassword: boolean = false;
  public password: string = '';
  public email: string = '';
  private callbackUrl = '/dashboard';

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private router: Router,
    private readonly snackbarService: MatSnackBar,
    private readonly changeDetectorRef: ChangeDetectorRef,
    private readonly authenticationService: AuthenticateService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      let callbackUrl = params['callbackUrl'];
      if (callbackUrl != null) {
        this.callbackUrl = callbackUrl;
      }
    });
    this.userService.currentUserSubject.subscribe((r) => {
      if (r != null) {
        void this.router.navigate([this.callbackUrl]);
      }
    });
  }

  async loginProcedure() {
    if (this.isInputCorrect()) {
      await this.login();
      this.verificationEmail();
    }
  }

  async login() {
    this.showLoading = true;
    await this.userService
      .login(this.email, this.password)
      .catch(() => {
        this.snackbarService.open('Login failed.', 'Dismiss', {
          duration: 5000,
        });
        this.showLoading = false;
      })
      .then(() => {
        this.showLoading = false;
      });
    await this.router.navigateByUrl(this.callbackUrl);
  }

  verificationEmail() {
    // Remind user to confirm email and resend confirmation email
    if (this.userService.currentUserSubject.value?.emailConfirmed == false) {
      let snackBarRef = this.snackbarService.open(
        'Please confirm your E-Mail address!',
        'Resend Verification E-Mail',
        { duration: 5000 }
      );
      snackBarRef.onAction().subscribe(() => {
        lastValueFrom(
          this.authenticationService.apiAuthenticateResendConfirmationEmailPost(
            {
              email: this.email,
            }
          )
        )
          .catch(() => {
            this.snackbarService.open('Failed to send email', 'OK');
          })
          .then(() => {
            this.snackbarService.open('Verification EMail sent!', 'OK');
          });
      });
    }
  }

  isInputCorrect(): boolean {
    this.validateEmail();
    this.showPasswordError = this.password == '';
    return !(this.showEmailError || this.showPasswordError);
  }

  validateEmail() {
    //RegEx for emails
    let regex = new RegExp(
      /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
    let emailIsCorrect = regex.test(this.email);
    this.showEmailError = this.email == '' || !emailIsCorrect;
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
}
