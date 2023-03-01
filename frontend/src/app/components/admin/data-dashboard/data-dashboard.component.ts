import { Component, OnInit } from '@angular/core';
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ActivatedRoute} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";
import {ChapterService} from "../../../../services/generated/services/chapter.service";
import {ChapterDetailItem} from "../../../../services/generated/models/chapter-detail-item";
@Component({
  selector: 'app-data-dashboard',
  templateUrl: './data-dashboard.component.html',
  styleUrls: ['./data-dashboard.component.scss']
})
export class DataDashboardComponent implements OnInit {

  public moduleInfo: ModuleDetailItem | undefined;
  public chapters: ChapterDetailItem[] | undefined;
  public isLoading: boolean = false;

  constructor(private readonly moduleService : ModuleService,
              private readonly chapterService : ChapterService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.isLoading = true;
    let moduleId : string = "";
    this.route.queryParams.subscribe(params => {
      moduleId = params['moduleId'];
      this.loadModuleInfo(moduleId).then(() => {
        this.isLoading = false;
      })
    })
  }

  async loadModuleInfo(moduleId:string) {
    await lastValueFrom(this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    })).then((data) => {
      if (data == null) return;
      this.moduleInfo = data;
      this.loadChapters();
    }).catch((error) => {
      console.log(error);
    });
  }

  async loadChapters() {
    await lastValueFrom(this.chapterService.apiChapterForModuleGet$Json({
      moduleId: this.moduleInfo?.moduleId
    })).then((data) => {
      if (data == null) return;
      this.chapters = data;
    }).catch((error) => {
      console.log(error);
    });
  }
}
