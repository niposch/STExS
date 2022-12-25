import {Component, Input, OnInit} from '@angular/core';
import {Module} from "../../../../../services/generated/models/module";
import {ModuleService} from "../../../../../services/generated/services/module.service";

@Component({
  selector: 'app-module-list',
  templateUrl: './module-list.component.html',
  styleUrls: ['./module-list.component.scss']
})
export class ModuleListComponent {
  @Input() moduleList:Array<Module>|null = null;

  constructor(private readonly moduleService:ModuleService) {}

  async loadModules(){
    await this.moduleService.apiModuleGetModulesUserIsAdminOfGet$Json({
    }).toPromise()
      .then(modules =>{
        this.moduleList = modules ?? []
      })
  }
}
