import {Component, Input, OnInit} from '@angular/core';
import {Module} from "../../../../../services/generated/models/module";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {UserService} from "../../../../services/user.service";

@Component({
  selector: 'app-module-list',
  templateUrl: './module-list.component.html',
  styleUrls: ['./module-list.component.scss']
})
export class ModuleListComponent {
  @Input()
  moduleList:Array<Module>|null = null;
}
