import { Component, OnInit } from '@angular/core';
import {UserService} from "../../../services/user.service";
import {Module} from "../../../../services/generated/models/module";
import * as module from "module";
import {ModuleService} from "../../../../services/generated/services/module.service";
import {firstValueFrom, lastValueFrom} from "rxjs";


@Component({
  selector: 'app-modules-admin',
  templateUrl: './modules-admin.component.html',
  styleUrls: ['./modules-admin.component.scss']
})
export class ModulesAdminComponent implements OnInit {
  userName: string = "";

  moduleList:Array<Module>|null = null;

  constructor(private readonly moduleService:ModuleService,
              private readonly userService:UserService) { }

  ngOnInit(): void {
    void this.loadModules()
  }

  async loadModules(){
    await this.moduleService.apiModuleGetModulesUserIsAdminOfGet$Json({
    }).toPromise()
      .then(modules =>{
        this.moduleList = modules ?? []
      })
  }
}
