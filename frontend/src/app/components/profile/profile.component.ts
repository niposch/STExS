import { Component, OnInit } from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  isEditing: boolean = false;
  userName: string = "";
  isAdmin: boolean = false;
  isTeacher = false;
  email: string = "";
  firstName: string = "";
  lastName: string = "";
  matrikelNummer: string = "";
  phoneNumber: string = "";

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
      if(roles?.includes("admin")){
        this.isAdmin = true;
        this.isTeacher = true;
      }
      else if (roles?.includes("teacher")){
        this.isAdmin = false;
        this.isTeacher = true;
      }
      else{
        this.isAdmin = false;
        this.isTeacher = false;
      }
    })
  }

}
