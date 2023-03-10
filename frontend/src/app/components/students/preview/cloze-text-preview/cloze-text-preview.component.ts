import {Component, Input, OnInit} from '@angular/core';
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {
  ClozeTextSubmissionDetailItem
} from "../../../../../services/generated/models/cloze-text-submission-detail-item";
import {ClozeTextExerciseDetailItem} from "../../../../../services/generated/models/cloze-text-exercise-detail-item";
import {lastValueFrom} from "rxjs";
import {ClozeTextSubmissionService} from "../../../../../services/generated/services/cloze-text-submission.service";
import {ClozeTextExerciseService} from "../../../../../services/generated/services/cloze-text-exercise.service";

@Component({
  selector: 'app-cloze-text-preview',
  templateUrl: './cloze-text-preview.component.html',
  styleUrls: ['./cloze-text-preview.component.scss']
})
export class ClozeTextPreviewComponent implements OnInit {

  @Input() exerciseId!: string;
  @Input() submissionId: string|null = null;

  public exercise: ClozeTextExerciseDetailItem | undefined | null;

  public submission: ClozeTextSubmissionDetailItem | undefined | null;
  constructor(
    private readonly clozeTextSubmissionService: ClozeTextSubmissionService,
    private readonly clozeTextService: ClozeTextExerciseService,
  ) { }

  ngOnInit(): void {
    void this.loadData();
  }

  async loadData():Promise<any>{
    this.exercise = await lastValueFrom(this.clozeTextService.apiClozeTextExerciseWithAnswersGet$Json({
      id: this.exerciseId
    }))

    if(this.submissionId != null){
      this.submission = await lastValueFrom(this.clozeTextSubmissionService.apiClozeTextSubmissionGetByIdGet$Json({
        submissionId: this.submissionId
      }))
        .catch(() => null);

      console.log(this.submission)
    }
  }
}
