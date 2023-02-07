import {Component, Input, OnInit} from '@angular/core';
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";
import {ModuleParticipationStatus} from "../../../../services/generated/models/module-participation-status";

@Component({
  selector: 'app-join-module',
  templateUrl: './join-module.component.html',
  styleUrls: ['./join-module.component.scss']
})
export class JoinModuleComponent implements OnInit{
  searchInput : string = "";
  @Input() infoText : string = "";
  public results: Array<ModuleDetailItem> | null = null;
  public partStats: Array<ModuleParticipationStatus> = [];
  public displayedColumns = ["part", "moduleName", "teacher", "actions"]

  moduleParticipationStatus = ModuleParticipationStatus;
  constructor(private readonly moduleService:ModuleService) { }

  ngOnInit(): void {
    this.search("")
  }

   search(searchString: string) {
     this.moduleService.apiModuleSearchGet$Json({
       search: searchString
     }).subscribe(async data => {
       this.results = data;
       for (let module of this.results) {

       }
     })
   }

   private getCurrentPartStatus(module : ModuleDetailItem) {
     return this.moduleService.apiModuleGetModuleParticipationStatusGet$Json({
         moduleId: module.moduleId
     })
   }
}
