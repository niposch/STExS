import { Component, OnInit } from '@angular/core';
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ActivatedRoute} from "@angular/router";
import {catchError, lastValueFrom, of} from "rxjs";
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";
import {ChapterService} from "../../../../services/generated/services/chapter.service";
import {ChapterDetailItem} from "../../../../services/generated/models/chapter-detail-item";
import {ExerciseService} from "../../../../services/generated/services/exercise.service";
import {ExerciseDetailItem} from "../../../../services/generated/models/exercise-detail-item";
import {GradingService} from "../../../../services/generated/services/grading.service";
import {ModuleReport} from "../../../../services/generated/models/module-report";
@Component({
  selector: 'app-data-dashboard',
  templateUrl: './data-dashboard.component.html',
  styleUrls: ['./data-dashboard.component.scss']
})
export class DataDashboardComponent implements OnInit {

  public moduleReport: ModuleReport | null | undefined;

  constructor(private readonly moduleService : ModuleService,
              private readonly chapterService : ChapterService,
              private readonly exerciseService : ExerciseService,
              private readonly gradingService: GradingService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    let moduleId : string = "";
    this.route.queryParams.subscribe(params => {
      moduleId = params['moduleId'];
      void this.loadReport(moduleId)
    })
  }

  async loadReport(moduleId:string){
    this.moduleReport = await lastValueFrom(this.gradingService.apiGradingModuleGet$Json({
      moduleId: moduleId
    })
      .pipe(catchError((err) => {
        return of(null);
      })));
  }
}
