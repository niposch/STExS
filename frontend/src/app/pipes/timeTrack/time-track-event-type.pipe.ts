import { Pipe, PipeTransform } from '@angular/core';
import {TimeTrackEventType} from "../../../services/generated/models/time-track-event-type";

@Pipe({
  name: 'timeTrackEventType'
})
export class TimeTrackEventTypePipe implements PipeTransform {

  transform(value: TimeTrackEventType | undefined, ...args: unknown[]): string {
    if(value === undefined) return "Unknown";
    switch (value) {
      case TimeTrackEventType.FinalSubmission: return "Final Submission";
      case TimeTrackEventType.TemporarySubmission: return "Temporary Submission";
      case TimeTrackEventType.TimeTrackStart: return "Start";
      case TimeTrackEventType.TimeTrackClosed: return "Closed";
      case TimeTrackEventType.TimeTrackLostContact: return "Loss of Contact";
    }
  }

}
