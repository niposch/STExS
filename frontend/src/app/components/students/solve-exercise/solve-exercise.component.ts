import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {ExerciseService} from "../../../../services/generated/services/exercise.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ExerciseDetailItem} from "../../../../services/generated/models/exercise-detail-item";
import {ExerciseType} from "../../../../services/generated/models/exercise-type";

@Component({
  selector: 'app-solve-exercise',
  templateUrl: './solve-exercise.component.html',
  styleUrls: ['./solve-exercise.component.scss']
})
export class SolveExerciseComponent implements OnInit {

  // @ts-ignore
  public exerciseList: Array<ExerciseDetailItem>;
  private chapterId : string = "";
  public isLoading: boolean = true;

  public currentExerciseNr: number = 0;
  public exerciseListLength: number = 0;

  public currentExerciseId: string = "";
  public currentExerciseType : ExerciseType = 0;


  public showCodeOutput : boolean = false;

  constructor(private readonly activatedRoute: ActivatedRoute,
              private readonly exerciseService: ExerciseService,
              public snackBar: MatSnackBar,
              ) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["chapterId"] != null){
        this.chapterId = params["chapterId"];
        this.loadChapter();
      }
    })
  }

  private loadChapter() {
    lastValueFrom(this.exerciseService.apiExerciseByChapterIdGet$Json({
      chapterId: this.chapterId
    })).catch( () => {
        this.snackBar.open("Could not load Exercises", "ok", {duration:4000});
      }
    ).then(data => {
      // @ts-ignore
      this.exerciseList = data;
      // @ts-ignore
      this.exerciseListLength = this.exerciseList.length;
      this.currentExerciseNr = 0;
      this.updateExerciseComp(0);
      this.isLoading = false;

      console.log(this.exerciseList)
    })
  }

  public updateExerciseComp(shift : number = 1) {
    if (this.currentExerciseNr + shift < 0 || this.currentExerciseNr + shift > this.exerciseListLength-1) return;
    this.currentExerciseNr = this.currentExerciseNr + shift;

    let currentExercise : ExerciseDetailItem = this.exerciseList[this.currentExerciseNr];
    this.currentExerciseId = currentExercise.id!;
    this.currentExerciseType = currentExercise.exerciseType!;

    this.updateCompType();
  }

  private updateCompType() {
    if (this.currentExerciseType == ExerciseType.CodeOutput) {
      this.showCodeOutput = false;
      setTimeout(() => {
        this.showCodeOutput = true;
      })
    }
  }
}
