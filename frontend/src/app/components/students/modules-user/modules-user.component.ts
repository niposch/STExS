import { Component, OnInit } from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ModuleService} from "../../../services/mockmodule.service";

@Component({
  selector: 'app-modules-user',
  templateUrl: './modules-user.component.html',
  styleUrls: ['./modules-user.component.scss']
})
export class ModulesUserComponent implements OnInit {
  constructor(private readonly userService: UserService, private readonly moduleService: ModuleService) { }
  userModules = this.userService.currentUserModules;
  moduleList = this.moduleService.MockModuleList;

  ngOnInit(): void {
  }

}
