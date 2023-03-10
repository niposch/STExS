import { Component, OnInit } from '@angular/core';
import {BaseExercise} from "../../../../../services/generated/models/base-exercise";
import {ExerciseReportItem} from "../../../../../services/generated/models/exercise-report-item";
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";
import {ActivatedRoute} from "@angular/router";
import {GradingService} from "../../../../../services/generated/services/grading.service";
import {lastValueFrom} from "rxjs";
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {MatDialog} from "@angular/material/dialog";
import {RevisionHistoryComponent, RevisionHistoryData} from "../revision-history/revision-history.component";
import { ComponentType } from '@angular/cdk/overlay';
import {GradingDialogComponent, GradingDialogData} from "../grading-dialog/grading-dialog.component";

@Component({
  selector: 'app-grading-exercise-dashboard',
  templateUrl: './grading-exercise-dashboard.component.html',
  styleUrls: ['./grading-exercise-dashboard.component.scss']
})
export class GradingExerciseDashboardComponent implements OnInit {

  public exercise:ExerciseDetailItem| null = null;
  public gradingResults: Array<ExerciseReportItem> | null = null;
  constructor(
    private readonly exerciseService: ExerciseService,
    private route: ActivatedRoute,
    private readonly gradingService: GradingService,
    public dialog: MatDialog
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
    this.dialog.open<GradingDialogComponent, GradingDialogData>(GradingDialogComponent, {
      data:{
        exerciseId: element!.exerciseId!,
        userId: element.userId!
      },
      maxHeight: '90vh',
      panelClass: 'grading-dialog'
    });
  }

  openSubmissionListDialog(element: ExerciseReportItem) {
    this.dialog.open<RevisionHistoryComponent, RevisionHistoryData>(RevisionHistoryComponent, {
      data:{
        exerciseId: this.exercise!.id!,
        userId: element.userId!
      },
      maxHeight: '90vh',
      panelClass: 'revision-history-dialog'
    })

  }
}
