import { Component, OnInit } from '@angular/core';
import {Module} from "../../../../services/generated/models/module";
import {ModuleService} from "../../../../services/generated/services/module.service";
import {UserService} from "../../../services/user.service";

@Component({
  selector: 'app-modules-admin',
  templateUrl: './modules-admin.component.html',
  styleUrls: ['./modules-admin.component.scss']
})
export class ModulesAdminComponent implements OnInit{
  userName: string = "";

  moduleList:Array<Module> | null = null;

  constructor(private readonly moduleService:ModuleService,
              private readonly userService:UserService) { }

  async loadModules(){
    await this.moduleService.apiModuleGetModulesUserIsAdminOfGet$Json({
    }).toPromise()
      .then(modules =>{
        this.moduleList = modules ?? []
      })
  }

  ngOnInit(): void {
    this.loadModules()
  }
}
