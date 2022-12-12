import { Component, OnInit } from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleService} from "../../../services/mockmodule.service";


@Component({
  selector: 'app-modules-admin',
  templateUrl: './modules-admin.component.html',
  styleUrls: ['./modules-admin.component.scss']
})
export class ModulesAdminComponent implements OnInit {
  userName: string = "";
  loggedIn: boolean = false;
  isAdmin: boolean = false;
  userModules: number[]=[];
  userFavouriteModules: boolean[]=[];

  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) { }
  moduleList = this.moduleService.MockModuleList;

  ngOnInit(): void {

  }

}
