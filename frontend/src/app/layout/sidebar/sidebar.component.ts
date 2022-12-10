import { Component, OnInit } from '@angular/core';
import {UserService} from "../../services/user.service";
import {ModuleService} from "../../services/mockmodule.service";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  userModules: number[]=[];
  userFavouriteModules: boolean[]=[];
  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) {
    console.log('Sidebar constructor called');
  }
  moduleList = this.moduleService.MockModuleList;

  ngOnInit(): void {
    this.userService.currentUser.subscribe(user => {
      if (user != null) {
	    console.log(user);
      this.userModules = user.currentUserModules;
      this.userFavouriteModules = user.favourite_modules;
      }
    })
  }

}
