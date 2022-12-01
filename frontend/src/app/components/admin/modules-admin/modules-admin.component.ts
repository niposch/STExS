import { Component, OnInit } from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleService} from "../../../services/mockmodule.service";


@Component({
  selector: 'app-modules-admin',
  templateUrl: './modules-admin.component.html',
  styleUrls: ['./modules-admin.component.scss']
})
export class ModulesAdminComponent implements OnInit {
  module_1 = "Kurs 1";
  module_2 = "Kurs 2";
  module_3 = "Kurs 3";
  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) { }
  userModules = this.userService.currentUserModules;
  moduleList = this.moduleService.MockModuleList;

  ngOnInit(): void {
  }

}
