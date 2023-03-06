import {
  ChangeDetectorRef,
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges
} from '@angular/core';
import {ParsonExerciseLineDetailItem} from "../../../../../services/generated/models/parson-exercise-line-detail-item";
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {ParsonPuzzleService} from "../../../../../services/generated/services/parson-puzzle.service";
import {lastValueFrom} from "rxjs";
import {ParsonExerciseDetailItem} from "../../../../../services/generated/models/parson-exercise-detail-item";

@Component({
  selector: 'app-solve-parson-puzzle',
  templateUrl: './solve-parson-puzzle.component.html',
  styleUrls: ['./solve-parson-puzzle.component.scss']
})
export class SolveParsonPuzzleComponent implements OnInit, OnChanges {

  @Input()
  public id: string | undefined;
  public possibleAnswers: ParsonExerciseLineDetailItem[]|null = null;
  public userSolution: ParsonExerciseLineDetailItem[]|null = null;

  public exercise: ParsonExerciseDetailItem|null | undefined;
  @Output()
  public solveChange:EventEmitter<any> = new EventEmitter<any>();
  timeTrackId: string = "";
  isSaving:boolean = false;
  constructor(
    private readonly changeDetectorRef: ChangeDetectorRef,
    private readonly exerciseService: ParsonPuzzleService
  ) { }

  ngOnInit(): void {
    if(this.id != null){
      void this.loadExercise(this.id);
    }
  }

  async loadExercise(exerciseId:string): Promise<ExerciseDetailItem|null>{
    return await lastValueFrom(this.exerciseService.apiParsonPuzzleGet$Json({
      id: exerciseId
    }))
      .catch(() => {
        return null;
      })
      .then((data) => {
        if(data == null){
          return null;
        }
        this.exercise = data;
        this.possibleAnswers = data.lines ?? null;
        this.userSolution = [];
        return data;
      })
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.id != null){
      this.exercise = null;
      this.possibleAnswers = null;
      this.userSolution = null;
      void this.loadExercise(this.id)
        .then((data) => {
          this.changeDetectorRef.detectChanges();
        });
    }
  }

  createNewSubmission(timeTrackId: string, isFinal: boolean) {

  }
}
