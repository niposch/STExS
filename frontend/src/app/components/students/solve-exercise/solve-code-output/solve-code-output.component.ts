import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnInit,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import {
  debounce,
  fromEvent,
  interval,
  lastValueFrom,
  map,
  Observable,
  Subscription,
} from 'rxjs';
import { CodeOutputService } from '../../../../../services/generated/services/code-output.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CodeOutputDetailItem } from '../../../../../services/generated/models/code-output-detail-item';
import { TimeTrackService } from '../../../../../services/generated/services/time-track.service';
import { CodeOutputSubmissionService } from '../../../../../services/generated/services/code-output-submission.service';
import { CodeOutputSubmissionDetailItem } from '../../../../../services/generated/models/code-output-submission-detail-item';

@Component({
  selector: 'app-solve-code-output',
  templateUrl: './solve-code-output.component.html',
  styleUrls: ['./solve-code-output.component.scss'],
})
export class SolveCodeOutputComponent implements AfterViewInit {
  @Input() id: string = '';
  public answerString: string | undefined;
  public exercise: CodeOutputDetailItem | null = {};
  private lastSubmission: CodeOutputSubmissionDetailItem | undefined;
  public timeTrackId: string | null | undefined;
  public isLoading: boolean = false;

  constructor(
    private readonly codeoutputService: CodeOutputService,
    private readonly codeoutputSubmissionService: CodeOutputSubmissionService,
    private readonly timeTrackService: TimeTrackService,
    public snackBar: MatSnackBar
  ) {}
  public answerChanged: Observable<string> = new Observable<string>((sub) => {
    this.answerChangedSub = sub;
  });
  private answerChangedSub: Subscription | undefined;

  @ViewChild('answerInputField') set answerInputField(
    input: ElementRef<HTMLInputElement>
  ) {
    if (input) {
      this.answerChangedSub?.unsubscribe();
      // select the value of the input field
      // add a debounce of 2 seconds
      this.answerChangedSub = fromEvent(input.nativeElement, 'keyup')
        .pipe(
          map((v) => input.nativeElement.value),
          debounce((i) => interval(4000))
        )
        .subscribe((value) => {
          console.log('value', value);
          this.tempSave();
        });
    }
  }

  ngAfterViewInit(): void {
    this.isLoading = true;
    void this.loadExercise().then(() => {
      if (!this.exercise?.userHasSolvedExercise) {
      }
    });
  }

  async loadExercise(): Promise<any> {
    await lastValueFrom(
      this.codeoutputService.apiCodeOutputGet$Json({
        id: this.id,
      })
    )
      .catch(() => {
        this.snackBar.open('Could not load this Code-Output Exercise', 'ok', {
          duration: 3000,
        });
      })
      .then((data) => {
        // @ts-ignore
        this.exercise = data;
        this.getTimeTrack(this.exercise!.id!).then(() => {
          this.isLoading = false;
        });
      });
  }

  private async getTimeTrack(eId: string): Promise<any> {
    if (this.exercise?.userHasSolvedExercise) return;

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
    return this.queryLastTempSolution(eId, this.timeTrackId!);
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

  public createNewSubmission(
    ttId: string | null | undefined,
    isFinal: boolean = false
  ) {
    lastValueFrom(
      this.codeoutputSubmissionService.apiCodeOutputSubmissionSubmitTimeTrackIdPost(
        {
          timeTrackId: ttId!,
          isFinalSubmission: isFinal,
          body: {
            submittedAnswer: this.answerString,
            exerciseId: this.exercise!.id!,
          },
        }
      )
    )
      .catch(() => {
        this.snackBar.open('Could not submit the answer!', 'dismiss');
      })
      .then(() => {
        if (isFinal) {
          this.exercise!.userHasSolvedExercise = true;
          this.closeTimeTrack(ttId!)
            .catch(() => {
              this.snackBar.open('Could not close TimeTrack', 'dismiss');
            })
            .then(() => {
              this.snackBar.open('Submitted answer successfully!', 'ok', {
                duration: 3000,
              });
            });
        }
      });
  }

  private closeTimeTrack(ttId: string) {
    return lastValueFrom(
      this.timeTrackService.apiTimeTrackClosePost({
        timeTrackId: ttId!,
      })
    );
  }

  ngOnDestroy(): void {
    this.tempSave();
  }

  tempSave() {
    this.createNewSubmission(this.timeTrackId, false);
  }
}
