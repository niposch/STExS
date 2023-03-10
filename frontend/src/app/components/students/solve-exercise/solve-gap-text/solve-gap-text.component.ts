import {
  OnInit,
  AfterViewInit,
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  Output,
  ViewChild,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ClozeTextExerciseDetailItem } from '../../../../../services/generated/models/cloze-text-exercise-detail-item';
import { ClozeTextExerciseService } from '../../../../../services/generated/services/cloze-text-exercise.service';
import { TimeTrackService } from '../../../../../services/generated/services/time-track.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ViewClozeComponent } from 'src/app/components/admin/exercise-admin/create-edit-cloze/view-cloze/view-cloze.component';
import { ClozeTextSubmissionService } from '../../../../../services/generated/services/cloze-text-submission.service';
import { ClozeTextSubmissionDetailItem } from '../../../../../services/generated/models/cloze-text-submission-detail-item';
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";

@Component({
  selector: 'app-solve-gap-text',
  templateUrl: './solve-gap-text.component.html',
  styleUrls: ['./solve-gap-text.component.scss'],
})
export class SolveGapTextComponent implements OnInit, OnDestroy {
  @Input() id: string = '';
  @Output() solvedChange: EventEmitter<ExerciseDetailItem> = new EventEmitter<ExerciseDetailItem>();

  public timeTrackId: string | null | undefined;

  public exercise?: ClozeTextExerciseDetailItem | null | undefined;

  public answerStrings: Array<string> | null = null;

  constructor(
    private readonly clozeTextService: ClozeTextExerciseService,
    private readonly clozetextSubmissionService: ClozeTextSubmissionService,
    private readonly timeTrackService: TimeTrackService,
    public snackBar: MatSnackBar,
    private readonly changeDetectorRef: ChangeDetectorRef
  ) {}

  //@ViewChild(ViewClozeComponent) child!: ViewClozeComponent;

  ngOnInit(): void {
    void this.initialLoad(this.id);
  }

  async initialLoad(exerciseId: string): Promise<any> {
    await this.loadExercise(exerciseId);
    if (this.exercise == null) return;
    if (this.exercise.userHasSolvedExercise) {
      await this.queryLastTempSolution(exerciseId, null);
      return;
    }

    await this.getTimeTrack(exerciseId);
    if (this.timeTrackId == null) return;
    await this.queryLastTempSolution(exerciseId, this.timeTrackId);
  }

  async loadExercise(id: string): Promise<any> {
    this.exercise = await lastValueFrom(
      this.clozeTextService.apiClozeTextExerciseWithoutAnswersGet$Json({
        id: id,
      })
    ).catch(() => {
      this.snackBar.open('Error: Could not load the exercise!', 'dismiss');
      return null;
    });
    this.changeDetectorRef.detectChanges();
  }

  private async getTimeTrack(eId: string): Promise<string | null> {
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

    return this.timeTrackId;
  }

  private async queryLastTempSolution(
    exerciseId: string,
    timetrackId: string | null
  ): Promise<any> {
    const lastTempSolution = await lastValueFrom(
      this.clozetextSubmissionService.apiClozeTextSubmissionGetClozeTextExerciseIdGet$Json(
        {
          clozeTextExerciseId: exerciseId,
          currentTimeTrackId: timetrackId ?? undefined,
        }
      )
    ).catch(() => {
      return null;
    });

    if (lastTempSolution == null) return;
    this.answerStrings = lastTempSolution.submittedAnswers ?? null;
  }

  public async createNewSubmission(
    ttId: string | null | undefined,
    isFinal: boolean = false,
    answer: string[] | undefined | null = null
  ): Promise<any> {
    if (this.exercise == null) return;
    const submission: ClozeTextSubmissionDetailItem = {
      exerciseId: this.exercise.id,
      submittedAnswers: answer,
    };

    let previousFinalState = this.exercise.userHasSolvedExercise;
    this.exercise.userHasSolvedExercise = isFinal;
    await lastValueFrom(
      this.clozetextSubmissionService.apiClozeTextSubmissionSubmitTimeTrackIdPost(
        {
          timeTrackId: ttId!,
          body: submission,
          isFinalSubmission: isFinal,
        }
      )
    ).then(() => {
      if (isFinal) this.solvedChange.emit(this.exercise!);

      this.snackBar.open('Submission saved!', 'dismiss', {
        duration: 2000,
      })
    })
      .catch(() => {
      this.snackBar.open('Error: Could not save the submission!', 'dismiss');
      if (this.exercise != null) {
        this.exercise.userHasSolvedExercise = previousFinalState;
      }
    });
  }

  ngOnDestroy(): void {
    if (this.exercise?.userHasSolvedExercise) return;
    if (this.answerStrings == null) return;
    this.tempSave(this.answerStrings!).then(() => {
      if (this.timeTrackId == null) return;
      this.closeTimeTrack(this.timeTrackId);
    });
  }

  private closeTimeTrack(ttId: string) {
    return lastValueFrom(
      this.timeTrackService.apiTimeTrackClosePost({
        timeTrackId: ttId!,
      })
    );
  }

  async tempSave(answerString: Array<string>): Promise<any> {
    if (answerString == undefined) return;
    if (this.exercise?.userHasSolvedExercise) return;
    console.log('tempSave', answerString);
    await this.createNewSubmission(this.timeTrackId, false, answerString);
  }
}
