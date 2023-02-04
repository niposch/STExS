import {Component, OnInit} from '@angular/core';
import {Module} from "../../../../services/generated/models/module";
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ModuleParticipationDetailItem} from "../../../../services/generated/models/module-participation-detail-item";
import {ModuleParticipationService} from "../../../../services/generated/services/module-participation.service";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-modules-admin',
  templateUrl: './modules-admin.component.html',
  styleUrls: ['./modules-admin.component.scss']
})
export class ModulesAdminComponent implements OnInit {
  userName: string = "";

  moduleList: Array<Module> | null = null;
  pendingParticipations: Array<ModuleParticipationDetailItem> | null = null;

  constructor(private readonly moduleService: ModuleService,
              private readonly moduleParticipantionService: ModuleParticipationService) {
  }

  async loadModules() {
    await this.moduleService.apiModuleGetModulesUserIsAdminOfGet$Json({}).toPromise()
      .then(modules => {
        this.moduleList = modules ?? []
      })
    this.loadParticipantReq();
  }

  public loadParticipantReq() {
    this.moduleParticipantionService.apiModuleParticipationForAdminUserGet$Json()
      .pipe(map(participations => participations.filter(p => !p.participationConfirmed)))
      .subscribe(res => {
        this.pendingParticipations = res
        console.log(res)
      })
  }

  ngOnInit(): void {
    this.loadModules();
  }
}
