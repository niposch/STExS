import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {RoleType} from "../../../services/generated/models/role-type";
import {AuthenticateService} from "../../../services/generated/services/authenticate.service";
import {firstValueFrom, lastValueFrom} from "rxjs";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  isEditingName: boolean = false;
  isAdmin: boolean = false;
  isTeacher = false;
  userName: string = "";
  newUserName: string = "";
  email: string = "";
  firstName: string = "";
  lastName: string = "";
  matrikelNummer: string = "";
  phoneNumber: string = "";
  savingInProgress: boolean = false;

  constructor(private readonly userService: UserService,
              private readonly authenticationService:AuthenticateService,
              private readonly router: Router) {
  }

  initUserDataLoading(){
    this.userService.currentUserSubject.subscribe(user => {
      if (user != null) {
        console.log(user);
        this.userName = user.userName ?? "";
        this.email = user.email ?? "";
        this.firstName = user.firstName ?? "";
        this.lastName = user.lastName ?? "";
        this.matrikelNummer = user.matrikelNumber ?? "";
        this.phoneNumber = user.phoneNumber ?? "";
      }
    });
  }
  ngOnInit(): void {
    this.initUserDataLoading();

    this.userService.currentRoles.subscribe(roles => {
      if(roles?.includes(RoleType.Admin)){
        this.isAdmin = true;
        this.isTeacher = true;
      }
      else if (roles?.includes(RoleType.Teacher)){
        this.isAdmin = false;
        this.isTeacher = true;
      }
      else{
        this.isAdmin = false;
        this.isTeacher = false;
      }
    })
  }

  editButtonClick() {
    this.isEditingName = !this.isEditingName;
    //for passing value to parent object
    if (this.newUserName == "") {
      this.newUserName = this.userName;
    }

    if (!this.isEditingName) {
      this.userName = this.newUserName;
      console.log(this.newUserName);
    }
  }

  async saveUserSettings() {
      if (this.savingInProgress) return;

      this.savingInProgress = true;
      //BACKEND API POST ROUTE to change user info
      await lastValueFrom(this.authenticationService.apiAuthenticateModifyMyProfilePost({
        body:{
          email:this.email,
          firstName:this.firstName,
          lastName: this.lastName,
          userName: this.userName,
          matrikelNumber: this.matrikelNummer,
          phoneNumber: this.phoneNumber
        }
      }));
      await lastValueFrom(this.userService.getUserDetails())

      this.savingInProgress = false;
      console.log("SAVED:\t" + this.userName + "\t" + this.firstName + "\t" + this.lastName + "\t" + this.email + "\t" + this.matrikelNummer + "\t" + this.phoneNumber)
  }
}
