import { Component, OnInit } from '@angular/core';
import {ChapterDetailItem} from "../../../../../services/generated/models/chapter-detail-item";
import {BaseExercise} from "../../../../../services/generated/models/base-exercise";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ChapterService} from "../../../../../services/generated/services/chapter.service";
import {Module} from "../../../../../services/generated/models/module";
import {ActivatedRoute} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";
import {ExerciseType} from "../../../../../services/generated/models";

@Component({
  selector: 'app-grading-module-dashboard',
  templateUrl: './grading-module-dashboard.component.html',
  styleUrls: ['./grading-module-dashboard.component.scss']
})
export class GradingModuleDashboardComponent implements OnInit {
  public chapterList: Array<ListItem> | null = null;
  public module: Module | null = null;
  public exerciseType = ExerciseType;
  constructor(
    private readonly moduleService: ModuleService,
    private readonly chapterService: ChapterService,
    private readonly exerciseService: ExerciseService,
    private route: ActivatedRoute
  ) { }

ngOnInit(): void {

  this.route.queryParams.subscribe(params => {

    void this.loadData(params['moduleid']);
  })
}

  async loadData(moduleId:string):Promise<any>{
    this.module = await this.loadModule(moduleId);
    this.chapterList = await this.loadChapterList(moduleId);
  }

  async loadModule(moduleId:string): Promise<Module>{
    return await lastValueFrom(this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    }))
  }
  async loadChapterList(moduleId:string): Promise<Array<ListItem>>{
    let chaptersInModule = await lastValueFrom(this.chapterService.apiChapterForModuleGet$Json({
      moduleId: moduleId
    }));
    let chapterList: Array<ListItem> = [];
    for (let chapter of chaptersInModule){
      let exercises = await lastValueFrom(this.exerciseService.apiExerciseByChapterIdGet$Json({
        chapterId: chapter.id
      }));
      chapterList.push({
        chapter: chapter,
        exercises: exercises
      })
    }
    return chapterList;
  }
}
interface ListItem{
  chapter:ChapterDetailItem,
  exercises: Array<BaseExercise>
}
