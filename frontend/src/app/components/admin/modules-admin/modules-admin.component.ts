import { Component, OnInit } from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleService} from "../../../services/mockmodule.service";


@Component({
  selector: 'app-modules-admin',
  templateUrl: './modules-admin.component.html',
  styleUrls: ['./modules-admin.component.scss']
})
export class ModulesAdminComponent implements OnInit {
  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) { }
  userModules = this.userService.currentUserModules;
  moduleList = this.moduleService.MockModuleList;
  isAdmin = true;

  ngOnInit(): void {
  }

}
