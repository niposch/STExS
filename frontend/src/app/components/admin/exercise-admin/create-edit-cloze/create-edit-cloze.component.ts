import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ClozeTextExerciseDetailItem} from "../../../../../services/generated/models/cloze-text-exercise-detail-item";
import {ClozeTextExerciseService} from "../../../../../services/generated/services/cloze-text-exercise.service";
import {catchError, lastValueFrom, of} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-create-edit-cloze',
  templateUrl: './create-edit-cloze.component.html',
  styleUrls: ['./create-edit-cloze.component.scss']
})
export class CreateEditClozeComponent implements OnInit {

  @Input()
  public exerciseId: string|null = null;

  public exercise: ClozeTextExerciseDetailItem|null|undefined = undefined; // undefined = loading, null = error
  public isEditingExercise: boolean = false;
  public isOnlyInspectingExercise: boolean = false;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly clozeTextExerciseService: ClozeTextExerciseService,
    private readonly toastService: MatSnackBar
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if (params["exerciseId"] != null) {
        this.exerciseId = params["exerciseId"];
        this.isEditingExercise = true;
        this.loadExercise(this.exerciseId!)
          .then(exercise => {
            this.exercise = exercise;
          })
      }
      else if(params["chapterId"] != null) {
        this.isEditingExercise = false;
        this.exercise = {
          chapterId: params["chapterId"],
          text: "",
          exerciseDescription: "",
          exerciseName: "",
          achievablePoints: 0,
        }
      }
      else {
        this.exercise = null;
      }
    })
  }

  async loadExercise(exerciseId: string): Promise<ClozeTextExerciseDetailItem|null> {
    return lastValueFrom(this.clozeTextExerciseService.apiClozeTextExerciseWithAnswersGet$Json({
      id: exerciseId
    })
      .pipe(map(exercise => {
        exercise.exerciseName ??= "";
        exercise.exerciseDescription ??= "";
        return exercise;
      }))
      .pipe(catchError(err => {
        console.error(err);
        return of(null);
      })));
  }

  goBack() {
    window.history.back();
  }

  async createExercise() {
    this.isEditingExercise = true;
    await lastValueFrom(this.clozeTextExerciseService.apiClozeTextExerciseCreatePost$Json({
      body:{
        chapterId: this.exercise?.chapterId,
        text: this.exercise?.text,
        exerciseDescription: this.exercise?.exerciseDescription,
        exerciseName: this.exercise?.exerciseName,
        achievablePoints: this.exercise?.achievablePoints
      }
    }))
  }

  async updateExercise() {
    // show toast that exercise was updated, or that an error occurred
    await lastValueFrom(this.clozeTextExerciseService.apiClozeTextExerciseUpdatePost({
      body: {
        ...this.exercise
      }
    }))
      .then(() => {
        this.toastService.open("Exercise updated", "OK", {
          duration: 2000
        });
      })
      .catch(err => {
        console.error(err);
        this.toastService.open("Error updating exercise", "OK", {
          duration: 2000
        });
      })
  }
}
