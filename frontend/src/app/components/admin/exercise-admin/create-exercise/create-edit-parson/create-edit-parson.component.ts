import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {CodeOutputService} from "../../../../../../services/generated/services/code-output.service";
import {catchError, lastValueFrom} from "rxjs";
import {ExerciseType} from "../../../../../../services/generated/models/exercise-type";
import {CdkDragDrop, CdkDragEnter, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import {ParsonExerciseDetailItem} from "../../../../../../services/generated/models/parson-exercise-detail-item";
import {ParsonExerciseCreateItem} from "../../../../../../services/generated/models/parson-exercise-create-item";
import {environment} from "../../../../../../environments/environment";
import {map} from "rxjs/operators";
import {ParsonPuzzleService} from "../../../../../../services/generated/services/parson-puzzle.service";
import {
  ParsonExerciseLineDetailItem
} from "../../../../../../services/generated/models/parson-exercise-line-detail-item";


@Component({
  selector: 'app-create-edit-parson',
  templateUrl: './create-edit-parson.component.html',
  styleUrls: ['./create-edit-parson.component.scss']
})
export class CreateEditParsonComponent implements OnInit {

  public exercise: ParsonExerciseDetailItem | null | undefined = undefined; // undefined means loading, null means error
  public newLine: string = "";
  public isLoading: boolean = false;

  public isOnlyInspecting: boolean = false;

  private chapterId: string | null = null;
  emptyList: ParsonExerciseLineDetailItem[] = [];
  constructor(private route: ActivatedRoute,
              private router: Router,
              private snackBar: MatSnackBar,
              private parsonPuzzleService: ParsonPuzzleService) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.route.queryParams.subscribe(params => {
      let exerciseId = params['exerciseId'];
      this.chapterId = params['chapterId'];
      this.isOnlyInspecting = params['inspecting'] == "true";

      if (exerciseId && !this.chapterId) {
        this.loadExercise(exerciseId)
          .then(ex => {
            this.exercise = ex
            this.isLoading = false;
          })
          .catch(err => {
            this.exercise = null;
            console.log(err)
          })
      } else if(this.chapterId && !exerciseId) {
        this.exercise = {
          chapterId: this.chapterId,
          achieveablePoints: 0,
          exerciseDescription: "",
          exerciseName: "",
          lines: []

        } as ParsonExerciseCreateItem
      }
      else{
        this.exercise = null;
      }
    })
  }

  async saveExercise() {
    if(this.exercise == null){
      return;
    }
    if (this.exercise.id == null) {
      await this.createExercise();
    } else {
      await this.updateExercise();
    }
  }

  async updateExercise(){
    this.isLoading = true;
    await lastValueFrom(this.parsonPuzzleService.apiParsonPuzzleUpdatePost({
      body:{
        ...this.exercise
      }
    }))
      .then(() => {
        this.snackBar.open("Successfully updated Exercise!", "Dismiss", {duration:2000});
        this.isLoading = false;
        this.goBack();
      })
      .catch(err => {
        this.snackBar.open("Could not update Exercise!", "Dismiss", {duration: 5000});
        throw err
      })
  }
  async createExercise() {
    this.isLoading = true;
    await lastValueFrom(this.parsonPuzzleService.apiParsonPuzzleCreatePost$Json({
      body:{
        ...(this.exercise as ParsonExerciseCreateItem)
      }
    }))
      .catch(err =>{
        this.snackBar.open("Could not create Exercise!", "Dismiss", {duration:5000});
        throw err
      }).then(() => {
      this.snackBar.open("Successfully created Exercise!", "Dismiss", {duration:2000});
      this.isLoading = false;
      this.goBack();
    });
  }

  public async goBack(){
    if(this.chapterId == null && this.exercise?.chapterId == null){
      // navigate back
      history.back();
      return;
    }
    await this.router.navigate(['/module/administrate/chapter'], {queryParams : {chapterId: this.chapterId ?? this.exercise?.chapterId}})
  }

  private async loadExercise(exerciseId:string):Promise<ParsonExerciseDetailItem> {
    return await lastValueFrom(this.parsonPuzzleService.apiParsonPuzzleWithAnswersGet$Json({
      exerciseId: exerciseId
    })
      .pipe(map(e => {
        e.lines ??= [];
        e.exerciseDescription ??= "";
        e.exerciseName ??= "";
        return e;
      })))
  }

  addLine() {
    this.exercise?.lines?.push({
      id: environment.EmptyGuid,
      text: this.newLine,
      indentation: 0
    });
    this.newLine = "";
    this.exercise!.achievablePoints = this.exercise?.lines?.length ?? 0;
  }

  public empty: ParsonExerciseLineDetailItem[] = [];
  drop($event: CdkDragDrop<any[], any>) {
    this.exercise?.lines?.splice($event.previousIndex, 1);
  }
}
