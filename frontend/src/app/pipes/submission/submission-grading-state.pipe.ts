import { Pipe, PipeTransform } from '@angular/core';
import {SubmissionGradingState} from "../../../services/generated/models";

@Pipe({
  name: 'submissionGradingState'
})
export class SubmissionGradingStatePipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    switch (value) {
      case SubmissionGradingState.NotGraded: return "Not Graded";
      case SubmissionGradingState.AutomaticGraded: return "Automatic Graded";
      case SubmissionGradingState.ManuallyGraded: return "Manually Graded";
      default: return "";
    }
  }

}
