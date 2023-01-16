import {Component, Input, OnInit} from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {} from "../../../../../services/generated/models/module-participation";
import {ModuleParticipationDetailItem} from "../../../../../services/generated/models/module-participation-detail-item";
import {ModuleParticipationService} from "../../../../../services/generated/services/module-participation.service";
import * as module from "module";
import {config, every, scan} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-member-list-admin',
  templateUrl: './member-list-admin.component.html',
  styleUrls: ['./member-list-admin.component.scss']
})
export class MemberListAdminComponent implements OnInit {

  @Input()
  public participations: Array<ModuleParticipationDetailItem>|null = null;
  displayedColumns = ["First Name", "Last Name", "Email", "UserName", "Matrikel Number", "Module Name", "Actions"];

  constructor(private readonly moduleService:ModuleService,
              private readonly moduleParticipationService:ModuleParticipationService,
              private readonly toastService: MatSnackBar) { }

  ngOnInit(): void {
  }


  async acceptUser(userId: any, moduleId: any):Promise<any> {
    await this.moduleParticipationService.apiModuleParticipationConfirmPost({
      userId: userId,
      moduleId:moduleId
    })
      .subscribe()
    this.participations = this.participations?.filter(el => el.userId != userId && el.moduleId != moduleId) ?? null
    this.toastService.open("User was accepted into the module!")
  }

  async rejectUser(userId: any, moduleId: any):Promise<any> {
    await this.moduleParticipationService.apiModuleParticipationRejectPost({
      userId: userId,
      moduleId:moduleId
    })
      .subscribe()
    this.participations = this.participations?.filter(el => el.userId != userId && el.moduleId != moduleId) ?? null
    this.toastService.open("User was rejected from joining the module!")
  }
}
