import { Component, OnInit } from '@angular/core';
import {UserService} from "../../services/user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  userName: string = "";
  isAdmin: boolean = false;
  isTeacher = false;

  constructor(private readonly userService: UserService,
              private readonly router: Router) {
  }

  ngOnInit(): void {
    this.userService.currentUserSubject.subscribe(user => {
      if (user != null) {
        console.log(user);
        this.userName = user.userName ?? "";
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
