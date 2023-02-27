import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {CodeOutputService} from "../../../../../../services/generated/services/code-output.service";
import {catchError, lastValueFrom} from "rxjs";
import {ExerciseType} from "../../../../../../services/generated/models/exercise-type";

@Component({
  selector: 'app-create-edit-code-output',
  templateUrl: './create-edit-code-output.component.html',
  styleUrls: ['./create-edit-code-output.component.scss']
})
export class CreateEditCodeOutputComponent implements OnInit {

  public expectedAnswer: string = "";
  public achievablePoints: number = 0;
  public isMultilineResponse : boolean = false;
  public exerciseId: string = "";
  public chapterId : string = "";
  public description : string = "";
  public name : string = "";
  public isEditingExercise = false;
  public isOnlyInspectingExercise = false;
  private runningNumber: number = 0;

  public loading = true

  constructor(private route: ActivatedRoute,
              private router: Router,
              private snackBar: MatSnackBar,
              private codeOutputService: CodeOutputService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.exerciseId = params['exerciseId'];
      this.chapterId = params['chapterId'];
      this.isOnlyInspectingExercise = params['inspecting'];

      if (this.exerciseId && !this.chapterId) {
        this.isEditingExercise = true;
        this.loadExercise(this.exerciseId)
      } else {
        this.isEditingExercise = false;
        this.loading = false
      }
    })
  }

  saveExercise() {
    if (this.isEditingExercise) {
      this.updateExercise();
    } else {
      this.createExercise();
    }
  }

  updateExercise(){
    this.codeOutputService.apiCodeOutputUpdatePost({
      body:{
        expectedAnswer: this.expectedAnswer,
        achievablePoints: this.achievablePoints,
        chapterId: this.chapterId,
        creationDate: null,
        exerciseDescription: this.description,
        exerciseName: this.name,
        exerciseType: ExerciseType.CodeOutput,
        id: this.exerciseId,
        isMultiLineResponse: this.isMultilineResponse,
        modificationDate: null,
        runningNumber: this.runningNumber
      }
    })
      .pipe(catchError(err => {
        this.snackBar.open("Could not update Exercise!", "Dismiss", {duration: 5000});
        throw err
      }))
      .subscribe(() => {
        this.snackBar.open("Successfully updated Exercise!", "Dismiss", {duration:2000});
        this.goBack();
      })
  }
  createExercise() {
    lastValueFrom(this.codeOutputService.apiCodeOutputCreatePost$Json({
      body:{
        expectedAnswer: this.expectedAnswer,
        isMultilineResponse: this.isMultilineResponse,
        exerciseName: this.name,
        exerciseDescription: this.description,
        achieveablePoints: this.achievablePoints,
        chapterId: this.chapterId,
      }
    }))
      .catch(err =>{
        this.snackBar.open("Could not create Exercise!", "Dismiss", {duration:5000});
        throw err
      }).then(() => {
      this.snackBar.open("Successfully created Exercise!", "Dismiss", {duration:2000});
      this.goBack();
    });
  }

  public async goBack(){
    await this.router.navigate(['/module/administrate/chapter'], {queryParams : {chapterId:this.chapterId}})
  }

  private async loadExercise(exerciseId:string) {
    await this.codeOutputService.apiCodeOutputWithAnswersGet$Json({
      id: exerciseId
    })
      .subscribe(res => {
          this.expectedAnswer = res.expectedAnswer ?? ""
          this.achievablePoints = res.achievablePoints ?? 0
          this.chapterId = res.chapterId ?? ""
          this.description = res.exerciseDescription ?? ""
          this.name = res.exerciseName ?? ""
          this.isMultilineResponse = res.isMultiLineResponse ?? false
          this.runningNumber  = res.runningNumber ?? 0
          this.loading = false
      })
  }
}
