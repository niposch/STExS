import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {ExerciseType} from "../../../../services/generated/models/exercise-type";
import {ExerciseDetailItem} from "../../../../services/generated/models/exercise-detail-item";
import {catchError, lastValueFrom, map, of} from "rxjs";
import {ExerciseService} from "../../../../services/generated/services/exercise.service";
import {GradingResultDetailItem} from "../../../../services/generated/models/grading-result-detail-item";
import {GradingService} from "../../../../services/generated/services/grading.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {GradingState} from "../../../../services/generated/models/grading-state";

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.scss']
})
export class PreviewComponent implements OnInit, OnChanges {

  @Input() exerciseId!: string;
  @Input() submissionId: string|null = null;
  @Input() exerciseType!: ExerciseType;

  public ExerciseType = ExerciseType;
  public exercise: ExerciseDetailItem | undefined | null;
  public grading: GradingResultDetailItem | undefined | null;

  constructor(
    private readonly exerciseService: ExerciseService,
    private readonly gradingService: GradingService,
    public snackBar : MatSnackBar,

  ){}

  async loadData(){
    this.exercise = await lastValueFrom(this.exerciseService.apiExerciseGet$Json({
      exerciseId: this.exerciseId
    }))
    if(this.submissionId != null){
      this.grading = await lastValueFrom(this.gradingService.apiGradingGradingForSubmissionGet$Json$Response({
        submissionId: this.submissionId
      }).pipe(map(resp =>{

        if(resp.status == 200) {
          return resp.body;
        }
        return null;
      }),
        catchError(resp => {
          if(resp.status != 404){
            return of(null);
          }

          console.log("Handeling 404")
          return of({
            comment: "",
            points: 0,
            gradingState:GradingState.NotGraded,
            gradedSubmissionId: this.submissionId
          } as GradingResultDetailItem)
        })))

    }
  }

  ngOnInit(): void {
    void this.loadData();
  }
  ngOnChanges(changes: SimpleChanges) {
    if(changes['exerciseId']?.currentValue != null){
      this.exerciseId = changes['exerciseId']?.currentValue;
    }
    this.submissionId = changes['submissionId']?.currentValue;
    if(changes['exerciseType']?.currentValue != null){
      this.exerciseType = changes['exerciseType']?.currentValue;
    }
  }

  gradeChange() {
    if(this.grading!.points! > this.exercise!.achievablePoints! ?? 0){
      this.grading!.points! = this.exercise?.achievablePoints!;
    }
    if(this.grading!.points! < 0){
      this.grading!.points! = 0;
    }
  }

  setGrade() {
    lastValueFrom(this.gradingService.apiGradingManualGradingPost({
      submissionId: this.submissionId!,
      newGrade: this.grading!.points!,
      comment: this.grading!.comment!,
    })).then(() => {
      this.snackBar.open("Grading successful!", "Close", {duration: 5000});
    }).catch((err) => {
      this.snackBar.open("Error while grading!\n" + err, "Close", {duration: 5000});
    })
  }
}
