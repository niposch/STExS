import {ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {lastValueFrom} from "rxjs";
import {ClozeTextExerciseDetailItem} from "../../../../../services/generated/models/cloze-text-exercise-detail-item";
import {ClozeTextExerciseService} from "../../../../../services/generated/services/cloze-text-exercise.service";
import {TimeTrackService} from "../../../../../services/generated/services/time-track.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ViewClozeComponent} from 'src/app/components/admin/exercise-admin/create-edit-cloze/view-cloze/view-cloze.component';

@Component({
  selector: 'app-solve-gap-text',
  templateUrl: './solve-gap-text.component.html',
  styleUrls: ['./solve-gap-text.component.scss']
})
export class SolveGapTextComponent implements OnInit {

  @Input() id: string = "";
  public text: string = "";
  //public splitted: Array <string> | null=null;
  public gaps: Array <string> | null=null;
  public showGaps: boolean = false;
  //@ViewChild(ViewClozeComponent) view: ViewClozeComponent;      // -> this.child.gaps

  public timeTrackId: string | null | undefined = null;
  public exercise: ClozeTextExerciseDetailItem | null = {};
  public isLoading : boolean = false;
  @Output() solvedChange : EventEmitter<any> = new EventEmitter<any>();

  constructor( private readonly clozeTextService : ClozeTextExerciseService,
               private readonly timeTrackService: TimeTrackService,
               public snackBar: MatSnackBar,
               private readonly changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.showGaps = false;
    this.isLoading = true;
    this.loadExercise().then(() => {
      /*this.splitText();
      this.initGaps();*/
    })
  }

  async loadExercise(): Promise<any> {
    try {
      this.exercise = await lastValueFrom(
        this.clozeTextService.apiClozeTextExerciseWithoutAnswersGet$Json({
          id: this.id,
        })
      );
      //console.log(this.exercise)
      this.text = this.exercise.text!;
      if (!this.exercise!.userHasSolvedExercise) {
        await this.getTimeTrack(this.exercise!.id!).then(() => {
          this.isLoading = false;
          this.changeDetectorRef.detectChanges();
        });
        await this.queryLastTempSolution(this.exercise!.id!, this.timeTrackId!);
      } else {
        /*let lastSubmission = await lastValueFrom(
          this.codeoutputSubmissionService.apiCodeOutputSubmissionGetCodeOutputExerciseIdGet$Json(
            {
              codeOutputExerciseId: this.exercise!.id!,
            }
          )
        );
        this.answerString = lastSubmission.submittedAnswer ?? '';*/
        this.isLoading = false;
        this.changeDetectorRef.detectChanges();
      }
    } catch (err) {
      this.snackBar.open('Could not load this Code-Output Exercise', 'ok', {
        duration: 3000,
      });
    }
  }

  private async getTimeTrack(eId: string): Promise<any> {
    this.timeTrackId = await lastValueFrom(
      this.timeTrackService.apiTimeTrackPost$Json({
        exerciseId: eId,
      })
    ).catch(() => {
      this.snackBar.open(
        'Error: Could not get a TimeTrack instance!',
        'dismiss'
      );
      return null;
    });
  }

  private async queryLastTempSolution(eId: string, ttId: string): Promise<any> {
/*    let createNewSubmission: boolean = false;
    await lastValueFrom(
      this.codeoutputSubmissionService.apiCodeOutputSubmissionGetCodeOutputExerciseIdGet$Json(
        {
          codeOutputExerciseId: eId,
          currentTimeTrackId: ttId,
        }
      )
    )
      .catch((err) => {
        if (err.status != 404) {
          this.snackBar.open(
            'Error: Something went wrong while getting the last Submission!',
            'dismiss'
          );
        } else {
          createNewSubmission = true;
        }
      })
      .then((data) => {
        if (!createNewSubmission) {
          this.lastSubmission = data!;
          this.answerString = this.lastSubmission!.submittedAnswer!;
        }
      });*/
  }

  public async createNewSubmission(
    ttId: string | null | undefined,
    isFinal: boolean = false,
    answer: string | undefined = undefined
  ) : Promise<any> {
 /*   try {
      this.isSubmitting = true;
      await lastValueFrom(
        this.codeoutputSubmissionService.apiCodeOutputSubmissionSubmitTimeTrackIdPost(
          {
            timeTrackId: ttId!,
            isFinalSubmission: isFinal,
            body: {
              submittedAnswer: answer,
              exerciseId: this.exercise!.id!,
            },
          }
        )
      )
      if (isFinal) {
        this.solvedChange.emit({solved: true, exerciseId: this.exercise!.id!});
        this.exercise!.userHasSolvedExercise = true;
      }
      this.isSubmitting = false;
    } catch (err) {
      this.snackBar.open('Error while submitting the answer!', 'dismiss');
    }*/
  }



  buttonClick() {
    this.showGaps = !this.showGaps;
  }

  ngOnDestroy(): void {
    if (this.exercise?.userHasSolvedExercise) return;
    this.tempSave(this.gaps!).then(() => {
      if (this.timeTrackId == null) return;
      this.closeTimeTrack(this.timeTrackId);
    })
  }

  private closeTimeTrack(ttId: string) {
    return lastValueFrom(
      this.timeTrackService.apiTimeTrackClosePost({
        timeTrackId: ttId!,
      })
    );
  }

  async tempSave(answerString: string[]) : Promise<any> {
    if (answerString == undefined) return;
    if (this.exercise?.userHasSolvedExercise) return;
    console.log('tempSave', answerString);
    //await this.createNewSubmission(this.timeTrackId, false, answerString);
  }

}
