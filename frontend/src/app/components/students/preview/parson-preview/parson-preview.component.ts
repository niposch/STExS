import {Component, Input, OnInit} from '@angular/core';
import {ParsonExerciseDetailItem} from "../../../../../services/generated/models/parson-exercise-detail-item";
import {
  ParsonPuzzleSubmissionDetailItem
} from "../../../../../services/generated/models/parson-puzzle-submission-detail-item";
import {lastValueFrom} from "rxjs";
import {ParsonPuzzleService} from "../../../../../services/generated/services/parson-puzzle.service";
import {
  ParsonPuzzleSubmissionService
} from "../../../../../services/generated/services/parson-puzzle-submission.service";

@Component({
  selector: 'app-parson-preview',
  templateUrl: './parson-preview.component.html',
  styleUrls: ['./parson-preview.component.scss']
})
export class ParsonPreviewComponent implements OnInit {

  @Input() exerciseId!: string;
  @Input() submissionId: string|null = null;

  public exercise: ParsonExerciseDetailItem | undefined | null;
  public submission: ParsonPuzzleSubmissionDetailItem | undefined | null;
  constructor(
    private readonly parsonExerciseService:ParsonPuzzleService,
    private readonly parsonSubmissionService:ParsonPuzzleSubmissionService,
  ) { }

  ngOnInit(): void {
    void this.loadData();
  }

  async loadData():Promise<any>{
    this.exercise = await lastValueFrom(this.parsonExerciseService.apiParsonPuzzleWithAnswersGet$Json({
      exerciseId: this.exerciseId
    }));

    if(this.submissionId != null){
      this.submission = await lastValueFrom(this.parsonSubmissionService.apiParsonPuzzleSubmissionGetByIdGet$Json({
        submissionId: this.submissionId
      }))
        .catch(() => null);
    }

    console.log(this.submission)
  }
}
