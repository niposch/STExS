import { Component, OnInit } from '@angular/core';
import {BaseExercise} from "../../../../../services/generated/models/base-exercise";
import {ExerciseReportItem} from "../../../../../services/generated/models/exercise-report-item";
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";
import {ActivatedRoute} from "@angular/router";
import {GradingService} from "../../../../../services/generated/services/grading.service";
import {lastValueFrom} from "rxjs";

@Component({
  selector: 'app-grading-exercise-dashboard',
  templateUrl: './grading-exercise-dashboard.component.html',
  styleUrls: ['./grading-exercise-dashboard.component.scss']
})
export class GradingExerciseDashboardComponent implements OnInit {

  public exercise:BaseExercise| null = null;
  public gradingResults: Array<ExerciseReportItem> | null = null;
  constructor(
    private readonly exerciseService: ExerciseService,
    private route: ActivatedRoute,
    private readonly gradingService: GradingService
  ) { }

  ngOnInit(): void {
    void this.loadData();
  }

  async loadData() {
    const exerciseId = this.route.snapshot.queryParamMap.get('exerciseId');
    if (exerciseId) {
      this.exercise = await this.loadExercise(exerciseId);
      this.gradingResults = await this.loadGradingResults(exerciseId);
    }
  }
  async loadExercise(exerciseId:string):Promise<BaseExercise> {
    return await lastValueFrom(this.exerciseService.apiExerciseGet$Json({
      exerciseId: exerciseId
    }))
  }
  async loadGradingResults(exerciseId:string):Promise<Array<ExerciseReportItem>> {
    return await lastValueFrom(this.gradingService.apiGradingExerciseGet$Json({
      exerciseId: exerciseId
    }))
  }

  openGradingDialog(element:ExerciseReportItem) {

  }

  openSubmissionListDialog(element: ExerciseReportItem) {

  }
}
