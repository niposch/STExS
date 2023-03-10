import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { lastValueFrom } from 'rxjs';
import { ExerciseService } from '../../../../services/generated/services/exercise.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ExerciseDetailItem } from '../../../../services/generated/models/exercise-detail-item';
import { ExerciseType } from '../../../../services/generated/models/exercise-type';
import { Location } from '@angular/common';

@Component({
  selector: 'app-solve-exercise',
  templateUrl: './solve-exercise.component.html',
  styleUrls: ['./solve-exercise.component.scss'],
})
export class SolveExerciseComponent implements OnInit {
  // @ts-ignore
  public exerciseList: Array<ExerciseDetailItem>;
  private chapterId: string = '';
  public isLoading: boolean = true;

  public currentExerciseNr: number = 0;
  public exerciseListLength: number = 0;

  public currentExerciseId: string = '';
  public currentExerciseType: ExerciseType = 0;
  public exerciseType = ExerciseType;
  public showSubmissionWindow: boolean = true;
  public showGrading: boolean = true;

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly router: Router,
    private readonly exerciseService: ExerciseService,
    private readonly changeDetectorRef: ChangeDetectorRef,
    public snackBar: MatSnackBar,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.queryParams.subscribe((params) => {
      if (params['chapterId'] != null) {
        this.chapterId = params['chapterId'];
        this.loadChapter();
      }
    });
  }

  private loadChapter() {
    lastValueFrom(
      this.exerciseService.apiExerciseByChapterIdGet$Json({
        chapterId: this.chapterId,
      })
    )
      .catch(() => {
        this.snackBar.open('Could not load Exercises', 'ok', {
          duration: 4000,
        });
      })
      .then((data) => {
        console.log(data);
        // @ts-ignore
        this.exerciseList = data;
        // @ts-ignore
        this.exerciseListLength = this.exerciseList.length;
        if (this.exerciseListLength == 0) {
          this.location.back();
          this.snackBar.open('This chapter contains no exercises!', 'ok', {
            duration: 4000,
          });
        }
        this.currentExerciseNr = 0;
        this.updateExerciseComp(0);
        this.isLoading = false;

        console.log(this.exerciseList);
      });
  }

  public updateExerciseComp(shift: number = 1) {
    if (
      this.currentExerciseNr + shift < 0 ||
      this.currentExerciseNr + shift > this.exerciseListLength - 1
    )
      return;
    this.currentExerciseNr = this.currentExerciseNr + shift;

    let currentExercise: ExerciseDetailItem =
      this.exerciseList[this.currentExerciseNr];
    this.currentExerciseId = currentExercise.id!;
    this.currentExerciseType = currentExercise.exerciseType!;

    this.updateCompType();
  }

  private updateCompType() {
    this.showSubmissionWindow = false;
    this.changeDetectorRef.detectChanges();
    this.showSubmissionWindow = true;
    this.changeDetectorRef.detectChanges();
  }

  public updateExerciseCompById(id: string | undefined) {
    this.currentExerciseId = id!;
    let currentExercise: ExerciseDetailItem = this.exerciseList.find(
      (e) => e.id == id
    )!;
    this.currentExerciseNr = this.exerciseList.indexOf(currentExercise);
    this.currentExerciseType = currentExercise.exerciseType!;
    this.updateCompType();
  }

  changeSolvedStatus($event: ExerciseDetailItem) {
    console.log($event);
    let index = this.exerciseList.findIndex((e) => {
      return e.id == $event.id;
    })
    this.exerciseList[index].userHasSolvedExercise = $event.userHasSolvedExercise;
    console.log(this.exerciseList)
    this.showGrading = false;
    this.changeDetectorRef.detectChanges();
    this.showGrading = true;
  }
}
