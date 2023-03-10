import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {
  CodeOutputExerciseDetailItemWithAnswer
} from "../../../../../services/generated/models/code-output-exercise-detail-item-with-answer";
import {
  CodeOutputSubmissionDetailItem
} from "../../../../../services/generated/models/code-output-submission-detail-item";
import {lastValueFrom} from "rxjs";
import {CodeOutputSubmissionService} from "../../../../../services/generated/services/code-output-submission.service";
import {CodeOutputService} from "../../../../../services/generated/services/code-output.service";

@Component({
  selector: 'app-code-output-preview',
  templateUrl: './code-output-preview.component.html',
  styleUrls: ['./code-output-preview.component.scss']
})
export class CodeOutputPreviewComponent implements OnInit {

  @Input() exerciseId!: string;
  @Input() submissionId: string|null = null;

  public exercise: CodeOutputExerciseDetailItemWithAnswer | null | undefined;
  public submission: CodeOutputSubmissionDetailItem | null | undefined;
  constructor(
    private readonly submissionService: CodeOutputSubmissionService,
    private readonly exerciseService: CodeOutputService
  ) { }

  ngOnInit(): void {
    void this.loadData()
  }

  async loadData(): Promise<any>{
    this.exercise = await lastValueFrom(this.exerciseService.apiCodeOutputWithAnswersGet$Json({
      id: this.exerciseId
    }))
      .catch((err) => null);

    if(this.submissionId != null){
      this.submission = await lastValueFrom(this.submissionService.apiCodeOutputSubmissionGetByIdGet$Json({
        submissionId: this.submissionId
      }))
        .catch((err) => null);
    }
  }
}
