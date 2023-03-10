import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {GradingResultDetailItem} from "../../../../../services/generated/models/grading-result-detail-item";
import {GradingService} from "../../../../../services/generated/services/grading.service";
import {lastValueFrom} from "rxjs";
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import { ExerciseService } from 'src/services/generated/services';

@Component({
  selector: 'app-points-panel',
  templateUrl: './points-panel.component.html',
  styleUrls: ['./points-panel.component.scss']
})
export class PointsPanelComponent implements OnInit, OnChanges {

  @Input()
  public exerciseId: string | null = null;

  public exercise: ExerciseDetailItem | undefined | null;

  public gradingResult: GradingResultDetailItem | undefined | null;

  constructor(
    private readonly gradingService: GradingService,
    private readonly exerciseService: ExerciseService,
  ) {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['exerciseId']){
      void this.loadData(changes['exerciseId'].currentValue ?? null);
    }
  }

  ngOnInit(): void {
    void this.loadData(this.exerciseId);
  }

  async loadData(exerciseId:string|null){
    if(exerciseId == null){
      return;
    }
    this.exercise = await lastValueFrom(this.exerciseService.apiExerciseGet$Json({
      exerciseId: exerciseId
    }))
      .catch(() => null);
    this.gradingResult = await lastValueFrom(
      this.gradingService.apiGradingGetLatestGradingForExerciseGet$Json({
        exerciseId: exerciseId
      })
    )
      .catch(() => null)
  }

}
