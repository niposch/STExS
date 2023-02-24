import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {CodeOutputService} from "../../../../../../services/generated/services/code-output.service";
import {catchError, lastValueFrom} from "rxjs";
import {ExerciseType} from "../../../../../../services/generated/models/exercise-type";
import {CdkDragDrop, CdkDragEnter, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';


@Component({
  selector: 'app-create-edit-parson',
  templateUrl: './create-edit-parson.component.html',
  styleUrls: ['./create-edit-parson.component.scss']
})
export class CreateEditParsonComponent implements OnInit {

  public expectedAnswer: string = "";
  public achievablePoints: number = 0;
  public isMultilineResponse : boolean = false;
  public exerciseId: string = "";
  public chapterId : string = "";
  public description : string = "";
  public name : string = "";
  public isEditingExercise = false;
  private runningNumber: number = 0;

  public loading = true

  public create_puzzles: string[] = [ "" ];
  public puzzles: string[] = [];
  public empty: string[] =  [];
  public draggingOutsideSourceList: boolean = false;
  public isEditingPuzzleName: boolean = true;
  public puzzleName : string = "";
  public newPuzzleName : string = "";

  constructor(private route: ActivatedRoute,
              private router: Router,
              private snackBar: MatSnackBar,
              private codeOutputService: CodeOutputService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.exerciseId = params['exerciseId'];
      this.chapterId = params['chapterId'];

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
        achieveablePoints: this.achievablePoints,
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
          this.achievablePoints = res.achieveablePoints ?? 0
          this.chapterId = res.chapterId ?? ""
          this.description = res.exerciseDescription ?? ""
          this.name = res.exerciseName ?? ""
          this.isMultilineResponse = res.isMultiLineResponse ?? false
          this.runningNumber  = res.runningNumber ?? 0
          this.loading = false
      })
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      //console.log(event);
      if ( [ "puzzle_list", "delete_puzzle_list" ].includes( event.container.id ) ) {
        //moved created puzzle into puzzle list or delete puzzle
        //allow creation of new puzzle
        this.isEditingPuzzleName = true;
      }
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
    }
    this.draggingOutsideSourceList = false;
    console.log(this.puzzles);
  }

  onCdkDragEntered(event: CdkDragEnter<string>) {
    this.draggingOutsideSourceList = event.container.id == "delete_puzzle_list" ? true : false;
  }

  createPuzzle() {
    this.puzzleName = this.newPuzzleName;
    this.create_puzzles[0] = this.puzzleName;
    this.isEditingPuzzleName = false;
  }

}
