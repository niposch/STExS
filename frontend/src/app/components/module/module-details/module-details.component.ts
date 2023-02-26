import {Component, OnInit} from '@angular/core';
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ActivatedRoute} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {ModuleParticipationStatus} from "../../../../services/generated/models/module-participation-status";
import {ChapterDetailItem} from "../../../../services/generated/models/chapter-detail-item";
import {ChapterService} from "../../../../services/generated/services/chapter.service";
import {MatSnackBar} from "@angular/material/snack-bar";

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
  showChapters: boolean = false;
  public showLoading: boolean = false;
  constructor(private readonly moduleService:ModuleService,
              private readonly chapterService:ChapterService,
              private readonly activatedRoute:ActivatedRoute,
              private snackBar: MatSnackBar) { }

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

    this.moduleService.apiModuleGetModuleParticipantCountGet$Json({
      moduleId: moduleId
    })
      .subscribe(count => this.moduleParticipantCount = count)

    await this.getParticipationStatus(moduleId);

    this.chapterService.apiChapterForModuleGet$Json({
      moduleId: moduleId
    })
      .subscribe(data => {
        this.chapterList = data;
        this.chapterList.sort( (a,b) => this.compareChapters(a,b) );
        if (this.chapterList.length > 0) {
          this.showChapters = true;
        }
      })
  }
  async joinModule(moduleId:string){
    this.showLoading = true;
    await this.moduleService.apiModuleJoinModulePost({
      moduleId: moduleId
    })
      .subscribe( async () => {
        await lastValueFrom(this.moduleService.apiModuleGetModuleParticipationStatusGet$Json({
          moduleId: moduleId
        })).catch(() => {
          this.snackBar.open('An error occurred', 'ok', {duration: 4000})
        })
          .then(value => {
            this.participationStatus = value!;
            if (this.participationStatus == ModuleParticipationStatus.Requested) {
              this.snackBar.open('Successfully requested joining!', 'dismiss', {duration: 3000})
            } else if (this.participationStatus == ModuleParticipationStatus.NotParticipating) {
              this.snackBar.open('Can\'t join this module', 'Ok', {duration: 4000});
            }
            this.showLoading = false;
          })
      })
  }

  async getParticipationStatus(moduleId: string) {
    await this.moduleService.apiModuleGetModuleParticipationStatusGet$Json({
      moduleId: moduleId
    })
      .subscribe(value => {
        this.participationStatus = value;
      })
  }

  private compareChapters(a : ChapterDetailItem = {}, b : ChapterDetailItem = {}) {
    // @ts-ignore
    if (a.runningNumber > b.runningNumber) {
      return 1
    } else { // @ts-ignore
      if (a.runningNumber < b.runningNumber) {
            return -1
          } else {
            return 0
          }
    }

  }
}
