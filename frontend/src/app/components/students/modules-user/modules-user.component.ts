import { Component, OnInit } from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleService} from "../../../services/mockmodule.service";

@Component({
  selector: 'app-modules-user',
  templateUrl: './modules-user.component.html',
  styleUrls: ['./modules-user.component.scss']
})
export class ModulesUserComponent implements OnInit {
  module_1 = "Kurs 1";
  module_2 = "Kurs 2";
  module_3 = "Kurs 3";
  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) { }
  userModules = this.userService.currentUserModules;
  moduleList = this.moduleService.MockModuleList;

  ngOnInit(): void {
  }

}
