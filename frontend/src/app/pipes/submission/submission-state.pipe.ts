import { Pipe, PipeTransform } from '@angular/core';
import {SubmissionState} from "../../../services/generated/models";

@Pipe({
  name: 'submissionState'
})
export class SubmissionStatePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    switch (value) {
      case SubmissionState.NotStarted: return "Not Started";
      case SubmissionState.StartedButNothingSubmitted: return "In Progress";
      case SubmissionState.FinalSolutionSubmitted: return "Final";
      case SubmissionState.TemporarySubmitted: return "Temporary Submitted";
      default: return "";
    }
  }

}
