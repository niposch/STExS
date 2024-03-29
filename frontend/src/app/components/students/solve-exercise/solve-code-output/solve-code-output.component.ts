import {
  AfterViewInit, ChangeDetectorRef,
  Component,
  ElementRef, EventEmitter,
  Input, OnDestroy, Output,
  ViewChild,
} from '@angular/core';
import {
  debounce,
  filter,
  fromEvent,
  interval,
  lastValueFrom,
  map,
} from 'rxjs';
import {CodeOutputService} from '../../../../../services/generated/services/code-output.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {CodeOutputDetailItem} from '../../../../../services/generated/models/code-output-detail-item';
import {TimeTrackService} from '../../../../../services/generated/services/time-track.service';
import {CodeOutputSubmissionService} from '../../../../../services/generated/services/code-output-submission.service';
import {
  CodeOutputSubmissionDetailItem
} from '../../../../../services/generated/models/code-output-submission-detail-item';
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";

@Component({
  selector: 'app-solve-code-output',
  templateUrl: './solve-code-output.component.html',
  styleUrls: ['./solve-code-output.component.scss'],
})
export class SolveCodeOutputComponent implements AfterViewInit, OnDestroy {
  @Input() id: string = '';
  public answerString: string | undefined;
  public exercise: CodeOutputDetailItem | null = {};
  private lastSubmission: CodeOutputSubmissionDetailItem | undefined;
  public timeTrackId: string | null | undefined = null;
  public isLoading: boolean = false;
  public isSubmitting: boolean = false;
  @Output() solvedChange : EventEmitter<ExerciseDetailItem> = new EventEmitter<ExerciseDetailItem>();

  constructor(
    private readonly codeoutputService: CodeOutputService,
    private readonly codeoutputSubmissionService: CodeOutputSubmissionService,
    private readonly timeTrackService: TimeTrackService,
    public snackBar: MatSnackBar,
    private readonly changeDetectorRef: ChangeDetectorRef
  ) {
  }

  @ViewChild('answerInputField') set answerInputField(
    input: ElementRef<HTMLInputElement>
  ) {
    if (input) {
      fromEvent(input.nativeElement, 'keyup')
        .pipe(
          map((v) => input.nativeElement.value),
          debounce((i) => interval(4000)),
          filter((i) => {
            return !this.exercise?.userHasSolvedExercise;
          })
        )
        .subscribe((value) => {
          console.log('value', value);
          if(this.timeTrackId == null) return;
          this.tempSave(value);
        });
    }
  }

  ngAfterViewInit(): void {
    this.isLoading = true;
    this.changeDetectorRef.detectChanges();
    void this.loadExercise();
  }

  async loadExercise(): Promise<any> {
    try {
      this.exercise = await lastValueFrom(
        this.codeoutputService.apiCodeOutputGet$Json({
          id: this.id,
        })
      );
      if (!this.exercise!.userHasSolvedExercise) {
        await this.getTimeTrack(this.exercise!.id!).then(() => {
          this.isLoading = false;
          this.changeDetectorRef.detectChanges();
        });
        await this.queryLastTempSolution(this.exercise!.id!, this.timeTrackId!);
      } else {
        let lastSubmission = await lastValueFrom(
          this.codeoutputSubmissionService.apiCodeOutputSubmissionGetCodeOutputExerciseIdGet$Json(
            {
              codeOutputExerciseId: this.exercise!.id!,
            }
          )
        );
        this.answerString = lastSubmission.submittedAnswer ?? '';
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
    let createNewSubmission: boolean = false;
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
      });
  }

  public async createNewSubmission(
    ttId: string | null | undefined,
    isFinal: boolean = false,
    answer: string | undefined = undefined
  ) : Promise<any> {
    try {
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
        this.solvedChange.emit({userHasSolvedExercise: true, id: this.exercise!.id!});
        this.exercise!.userHasSolvedExercise = true;
      }
      this.snackBar.open('Submission saved!', 'dismiss', {
        duration: 2000,
      })
      this.isSubmitting = false;
    } catch (err) {
      this.snackBar.open('Error while submitting the answer!', 'dismiss');
    }
  }

  private closeTimeTrack(ttId: string) {
    return lastValueFrom(
      this.timeTrackService.apiTimeTrackClosePost({
        timeTrackId: ttId!,
      })
    );
  }

  ngOnDestroy(): void {
    if (this.exercise?.userHasSolvedExercise) return;
    this.tempSave(this.answerString).then(() => {
      if (this.timeTrackId == null) return;
      this.closeTimeTrack(this.timeTrackId);
    })
  }

  async tempSave(answerString?: string) : Promise<any> {
    if (answerString == undefined) return;
    if (this.exercise?.userHasSolvedExercise) return;
    console.log('tempSave', answerString);
    await this.createNewSubmission(this.timeTrackId, false, answerString);
  }
}
