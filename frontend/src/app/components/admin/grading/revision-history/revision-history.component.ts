import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA} from "@angular/material/dialog";
import {lastValueFrom} from "rxjs";
import {GradingService} from "../../../../../services/generated/services/grading.service";
import {TimeTrackService} from "../../../../../services/generated/services/time-track.service";
import {TimeTrackEvent} from "../../../../../services/generated/models/time-track-event";

@Component({
  selector: 'app-revision-history',
  templateUrl: './revision-history.component.html',
  styleUrls: ['./revision-history.component.scss']
})
export class RevisionHistoryComponent implements OnInit {
  public events: Array<TimeTrackEvent> | undefined | null;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: RevisionHistoryData,
    private readonly timeTrackService: TimeTrackService

  ) { }

  ngOnInit(): void {
    void this.loadRevisionData();
  }

  async loadRevisionData():Promise<any>{
    let revisions = await lastValueFrom(
      this.timeTrackService.apiTimeTrackTimeTracksForExerciseAndUserGet$Json({
        exerciseId: this.data.exerciseId,
        userId: this.data.userId
      })
    )
      .catch(error => {
        console.log(error);
        return null;
      })
    console.log(revisions);
    this.events = revisions;
  }
}

export interface RevisionHistoryData{
  exerciseId: string;
  userId: string
}
