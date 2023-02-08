import {Component, Input, OnInit} from '@angular/core';
import {lastValueFrom} from "rxjs";
import {CodeOutputService} from "../../../../../services/generated/services/code-output.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {CodeOutputDetailItem} from "../../../../../services/generated/models/code-output-detail-item";
import {TimeTrackService} from "../../../../../services/generated/services/time-track.service";
import {CodeOutputSubmissionService} from "../../../../../services/generated/services/code-output-submission.service";
import {CodeOutputSubmissionDetailItem} from "../../../../../services/generated/models/code-output-submission-detail-item";
@Component({
  selector: 'app-solve-code-output',
  templateUrl: './solve-code-output.component.html',
  styleUrls: ['./solve-code-output.component.scss']
})
export class SolveCodeOutputComponent implements OnInit {

  @Input() id: string = "";
  public answerString : string | undefined;
  public exercise : CodeOutputDetailItem | null = {};
  private lastSubmission : CodeOutputSubmissionDetailItem | undefined;
  public timeTrackId : string | void | undefined;
  public isLoading : boolean = false;

  constructor(private readonly codeoutputService: CodeOutputService,
              private readonly codeoutputSubmissionService : CodeOutputSubmissionService,
              private readonly timeTrackService: TimeTrackService,
              public snackBar: MatSnackBar,) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadExercise();
  }

  loadExercise() {
    lastValueFrom(this.codeoutputService.apiCodeOutputGet$Json({
      id: this.id
    })).catch(() => {
        this.snackBar.open("Could not load this Code-Output Exercise", "ok", {duration: 3000});
      }
    ).then(data => {
      // @ts-ignore
      this.exercise = data;
      this.getTimeTrack(this.exercise!.id!);
      this.isLoading = false;
    })
  }

  private getTimeTrack(eId : string) {
    console.log(eId);
    lastValueFrom(this.timeTrackService.apiTimeTrackPost$Json( {
      exerciseId: eId
    })).catch(() => {
      this.snackBar.open('Error: Could not get a TimeTrack instance!', 'dismiss');
    }).then( data => {
      this.timeTrackId = data;
      this.queryLastTempSolution(eId, this.timeTrackId!);
    })
  }

  private queryLastTempSolution(eId: string, ttId: string) {
    let createNewSubmission : boolean = false;
    lastValueFrom(this.codeoutputSubmissionService.apiCodeOutputSubmissionGetCodeOutputExerciseIdGet$Json( {
      codeOutputExerciseId: eId,
      currentTimeTrackId: ttId
    })).catch( err => {
      if (err.status != 404) {
        this.snackBar.open('Error: Something went wrong while getting the last Submission!', 'dismiss')
      } else {
        createNewSubmission = true;
      }
    }).then( data => {
     if (!createNewSubmission) {
        this.lastSubmission = data!;
        this.answerString = this.lastSubmission!.submittedAnswer!;
      }
    })
  }

  public createNewSubmission(ttId: string | void, isFinal: boolean = false) {
    lastValueFrom(this.codeoutputSubmissionService.apiCodeOutputSubmissionSubmitTimeTrackIdPost( {
      timeTrackId: ttId!,
      isFinalSubmission: isFinal,
      body: {
        submittedAnswer : this.answerString,
        exerciseId: this.exercise!.id!
      }
    })).catch( () => {
      this.snackBar.open('Could not submit the answer!', 'dismiss');
    }).then( () => {
      if (isFinal){
        this.closeTimeTrack(ttId).catch(() => {
          this.snackBar.open('Could not close TimeTrack', 'dismiss');
        }).then( () => {
          this.snackBar.open("Submitted answer successfully!", 'ok', {duration: 3000});
        });
      }
    })
  }

  private closeTimeTrack(ttId: string | void) {
    return lastValueFrom(this.timeTrackService.apiTimeTrackClosePost({
      timeTrackId: ttId!
    }))
  }
}
