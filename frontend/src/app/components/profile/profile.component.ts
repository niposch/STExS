import {Component, OnInit} from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";
import {RoleType} from "../../../services/generated/models/role-type";

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
  hasSaved: boolean = false;

  constructor(private readonly userService: UserService,
              private readonly router: Router) {
  }

  ngOnInit(): void {
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
      if (this.hasSaved) return;

      this.hasSaved = true;
      //BACKEND API POST ROUTE to change user info
      await new Promise(f => setTimeout(f, 1000));


      this.hasSaved = false;
      console.log("SAVED:\t" + this.userName + "\t" + this.firstName + "\t" + this.lastName + "\t" + this.email + "\t" + this.matrikelNummer + "\t" + this.phoneNumber)
  }
}
