import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Module} from "../../../../../services/generated/models/module";
import {ModuleService} from "../../../../../services/generated/services/module.service";

@Component({
  selector: 'app-module-list',
  templateUrl: './module-list.component.html',
  styleUrls: ['./module-list.component.scss']
})
export class ModuleListComponent {
  @Input()
  moduleList:Array<Module>|null = null;

  @Input()
  isModuleAdmin = false;
  @Output()
  public moduleChange:EventEmitter<any> = new EventEmitter<any>();
}
