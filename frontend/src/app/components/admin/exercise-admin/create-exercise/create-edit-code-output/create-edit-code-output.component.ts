import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {CodeOutputService} from "../../../../../../services/generated/services/code-output.service";
import {lastValueFrom} from "rxjs";

@Component({
  selector: 'app-create-edit-code-output',
  templateUrl: './create-edit-code-output.component.html',
  styleUrls: ['./create-edit-code-output.component.scss']
})
export class CreateEditCodeOutputComponent implements OnInit {

  public expectedAnswer: string = "";
  public isMultilineResponse : boolean = false;
  public exerciseId: string = "";
  public chapterId : string = "";
  public description : string = "";
  public name : string = "";
  public isEditingExercise = false;

  constructor(private route: ActivatedRoute,
              private snackBar: MatSnackBar,
              private codeOutputService: CodeOutputService) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.exerciseId = params['exerciseId'];
      this.chapterId = params['chapterId'];
      this.exerciseId = params['doCreate'];

      if (this.exerciseId && !this.chapterId && !this.exerciseId) {
        this.isEditingExercise = true;
      } else {
        this.isEditingExercise = false;
      }
    })
  }

  createExercise() {
    lastValueFrom(this.codeOutputService.apiCodeOutputCreatePost$Json({
      body:{
        exerciseDescription: this.description,
        exerciseName: this.name,
        chapterId: this.chapterId,
      }
    }))
      .catch(err =>{
        this.snackBar.open("Could not create Exercise", "Dismiss", {duration:5000});
        throw err
      }).then(() => {
      this.snackBar.open("Successfully created Exercise", "Dismiss", {duration:2000});
    });
  }

}
