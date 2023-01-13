import { Component, OnInit } from '@angular/core';
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ActivatedRoute} from "@angular/router";
import {catchError, firstValueFrom, lastValueFrom} from "rxjs";
import {ModuleParticipationStatus} from "../../../../services/generated/models/module-participation-status";
import {ChapterDetailItem} from "../../../../services/generated/models/chapter-detail-item";
import {ChapterService} from "../../../../services/generated/services/chapter.service";

@Component({
  selector: 'app-module-details',
  templateUrl: './module-details.component.html',
  styleUrls: ['./module-details.component.scss']
})
export class ModuleDetailsComponent implements OnInit {
  public module:ModuleDetailItem | null | undefined = undefined;

  public chapterList:Array<ChapterDetailItem> | null | undefined = undefined;
  public moduleParticipationStatus = ModuleParticipationStatus

  public moduleParticipantCount:number | null = null;

  public participationStatus: ModuleParticipationStatus | null = null;
  constructor(private readonly moduleService:ModuleService,
              private readonly chapterService:ChapterService,
              private readonly activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params =>{
      if(params["id"] != null) {
        this.module = null
        this.participationStatus = null
        void this.loadModule(params["id"])
        return;
      }
    })
  }

  async loadModule(moduleId:string){
    await lastValueFrom(this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    }))
      .catch(err => {
        this.module = null;
        throw err;
      })
      .then(data => {
        this.module = data
      })
    this.moduleService.apiModuleGetModuleParticipationStatusGet$Json({
      moduleId: moduleId
    })
      .subscribe(value => this.participationStatus = value)
    this.moduleService.apiModuleGetModuleParticipantCountGet$Json({
      moduleId: moduleId
    })
      .subscribe(count => this.moduleParticipantCount = count)

    this.chapterService.apiChapterForModuleGet$Json({
      moduleId: moduleId
    })
      .subscribe(data => {
        this.chapterList = data;
      })
  }
  async joinModule(moduleId:string){
    await this.moduleService.apiModuleJoinModulePost({
      moduleId: moduleId
    })
      .subscribe()
    await this.loadModule(this.module?.moduleId!);
  }
}
