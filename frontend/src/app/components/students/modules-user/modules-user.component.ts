import { Component, OnInit } from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleService} from "../../../services/mockmodule.service";

@Component({
  selector: 'app-modules-user',
  templateUrl: './modules-user.component.html',
  styleUrls: ['./modules-user.component.scss']
})
export class ModulesUserComponent implements OnInit {
  userName: string = "";
  loggedIn: boolean = false;
  isAdmin: boolean = false;
  userModules: number[]=[];
  userFavouriteModules: boolean[]=[];
  
  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) { }
  moduleList = this.moduleService.MockModuleList;

  ngOnInit(): void {
    this.userService.currentUser.subscribe(user => {
      if (user != null) {
	    console.log(user);
        this.userName = user.userName;
        this.loggedIn = true;
		this.isAdmin = user.isAdmin;
		this.userModules = user.currentUserModules;
		this.userFavouriteModules = user.favourite_modules;
		}
    })
  }

}
