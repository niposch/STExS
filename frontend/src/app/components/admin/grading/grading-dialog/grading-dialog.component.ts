import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {RevisionHistoryData} from "../revision-history/revision-history.component";
import {GradingService} from "../../../../../services/generated/services/grading.service";
import {SubmissionDetailItem} from "../../../../../services/generated/models/submission-detail-item";
import {catchError, lastValueFrom, of} from "rxjs";

@Component({
  selector: 'app-grading-dialog',
  templateUrl: './grading-dialog.component.html',
  styleUrls: ['./grading-dialog.component.scss']
})
export class GradingDialogComponent implements OnInit {

  public submissions: Array<SubmissionDetailItem> | undefined | null = null;
  constructor(@Inject(MAT_DIALOG_DATA) public data: GradingDialogData,
              private readonly gradingService: GradingService) {
  }

  ngOnInit(): void {
    void this.loadSubmissions();
  }

  async loadSubmissions():Promise<any> {
    this.submissions = await lastValueFrom(this.gradingService.apiGradingSubmissionsForUserAndExerciseGet$Json({
      exerciseId: this.data.exerciseId,
      userId: this.data.userId
    })
      .pipe(catchError((err) => {
        console.error(err);
        return of(null);
      })));
  }
}

export interface GradingDialogData {
  exerciseId: string,
  userId: string
}
