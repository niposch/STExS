import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  Output,
  SimpleChange,
  SimpleChanges,
} from '@angular/core';
import { ParsonExerciseLineDetailItem } from '../../../../../services/generated/models/parson-exercise-line-detail-item';
import { ExerciseDetailItem } from '../../../../../services/generated/models/exercise-detail-item';
import { ParsonPuzzleService } from '../../../../../services/generated/services/parson-puzzle.service';
import { lastValueFrom } from 'rxjs';
import { ParsonExerciseDetailItem } from '../../../../../services/generated/models/parson-exercise-detail-item';
import { TimeTrackService } from '../../../../../services/generated/services/time-track.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ParsonPuzzleSubmissionService } from '../../../../../services/generated/services/parson-puzzle-submission.service';
import { ExerciseService } from '../../../../../services/generated/services/exercise.service';

@Component({
  selector: 'app-solve-parson-puzzle',
  templateUrl: './solve-parson-puzzle.component.html',
  styleUrls: ['./solve-parson-puzzle.component.scss'],
})
export class SolveParsonPuzzleComponent
  implements OnInit, OnChanges, OnDestroy
{
  @Input()
  public id: string | undefined;
  public possibleAnswers: ParsonExerciseLineDetailItem[] | null = null;
  public userSolution: ParsonExerciseLineDetailItem[] | null = null;

  public exercise: ParsonExerciseDetailItem | null | undefined;
  @Output()
  public solveChange: EventEmitter<any> = new EventEmitter<any>();
  timeTrackId: string | null = null;
  isSaving: boolean = false;
  private tempSaving: number = null!;
  constructor(
    private readonly changeDetectorRef: ChangeDetectorRef,
    private readonly parsonExerciseService: ParsonPuzzleService,
    private readonly timeTrackService: TimeTrackService,
    private readonly snackBar: MatSnackBar,
    private readonly parsonSubmissionService: ParsonPuzzleSubmissionService,
    private readonly exerciseService: ExerciseService
  ) {}

  ngOnDestroy(): void {
    if (this.tempSaving != null) {
      window.clearInterval(this.tempSaving);
    }

    if (this.isUserIsWorkingOnExercise()) {
      void this.saveTemporarily();
    }
  }

  ngOnInit(): void {
    if (this.id != null) {
      void this.initialLoad(this.id);
    }
    this.tempSaving = window.setInterval(() => {
      void this.saveTemporarily();
    }, 30 * 1000);
  }

  async loadExercise(exerciseId: string): Promise<ExerciseDetailItem | null> {
    return await lastValueFrom(
      this.parsonExerciseService.apiParsonPuzzleGet$Json({
        id: exerciseId,
      })
    ).catch(() => {
      return null;
    });
  }

  private async initialLoad(exerciseId: string): Promise<any> {
    this.exercise = await this.loadExercise(exerciseId);
    if (this.exercise == null) {
      return;
    }

    if (!this.exercise?.userHasSolvedExercise) {
      this.timeTrackId = await this.getTimeTrack(exerciseId);
    }
    this.possibleAnswers = this.exercise?.lines ?? null;
    this.userSolution = [];
    this.exercise = this.exercise ?? null;
    await this.tryLoadingLastSubmission(exerciseId, this.timeTrackId);
  }
  async tryLoadingLastSubmission(
    exerciseId: string,
    timeTrackId: string | null
  ) {
    let tempSolution = await lastValueFrom(
      this.parsonSubmissionService.apiParsonPuzzleSubmissionGetParsonPuzzleExerciseIdGet$Json(
        {
          parsonPuzzleExerciseId: exerciseId,
          currentTimeTrackId: this.timeTrackId ?? undefined,
        }
      )
    ).catch(() => {
      return null;
    });
    if (tempSolution != null && tempSolution.submittedLines != null) {
      this.userSolution = tempSolution.submittedLines;
      this.possibleAnswers =
        this.possibleAnswers?.filter((x) => {
          this.userSolution?.find((y) => y.id == x.id) == null;
        }) ?? [];
    }
    this.changeDetectorRef.detectChanges();
  }
  private isUserIsWorkingOnExercise(): boolean {
    return (
      this.exercise != null &&
      this.timeTrackId != null &&
      this.exercise.userHasSolvedExercise != true
    );
  }

  private async handleExerciseChange(newExerciseId: string) {
    if (this.isUserIsWorkingOnExercise()) {
      await this.createNewSubmission(
        this.timeTrackId!,
        false,
        this.userSolution!
      );
      await this.closeTimeTrack(this.timeTrackId!);
    }
    await this.initialLoad(newExerciseId);
  }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['id'] != null) {
      // new exercise to work on
      void this.handleExerciseChange(changes['id'].currentValue);
    }
  }

  private async saveTemporarily() {
    if (this.isUserIsWorkingOnExercise()) {
      await this.createNewSubmission(
        this.timeTrackId!,
        false,
        this.userSolution!
      );
      this.snackBar.open('Saved temporarily!', 'dismiss');
    }
  }

  createNewSubmission(
    timeTrackId: string | null,
    isFinal: boolean,
    userSolution: ParsonExerciseLineDetailItem[]
  ) {
    if (timeTrackId == null) {
      this.snackBar.open(
        'Error: Could not create a new Submission!',
        'dismiss'
      );
      return;
    }
    void lastValueFrom(
      this.parsonSubmissionService.apiParsonPuzzleSubmissionSubmitTimeTrackIdPost(
        {
          body: {
            exerciseId: this.exercise!.id,
            submittedLines: userSolution,
          },
          timeTrackId: timeTrackId,
          isFinalSubmission: isFinal,
        }
      )
    )
      .catch(() => {
        this.snackBar.open(
          'Error: Could not create a new Submission!',
          'dismiss'
        );
      })
      .then((data) => {
        if (isFinal && this.exercise) {
          this.exercise.userHasSolvedExercise = true;
        }
      });
  }

  private async closeTimeTrack(timeTrackId: string) {
    await lastValueFrom(
      this.timeTrackService.apiTimeTrackClosePost({
        timeTrackId: timeTrackId,
      })
    );
  }

  private async getTimeTrack(eId: string): Promise<string> {
    return await lastValueFrom(
      this.timeTrackService.apiTimeTrackPost$Json({
        exerciseId: eId,
      })
    );
  }
}
