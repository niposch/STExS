import {Pipe, PipeTransform} from '@angular/core';
import {GradingState} from "../services/generated/models/grading-state";

@Pipe({
  name: 'gradingState'
})
export class GradingStatePipe implements PipeTransform {

  transform(value: GradingState | undefined, ...args: unknown[]): string {
    switch (value) {
      case GradingState.NotGraded:
        return "Not Graded";
      case GradingState.AutomaticallyGraded:
        return "Automatically Graded";
      case GradingState.FinallyManuallyGraded:
        return "Manually Graded";
      case GradingState.NotGraded:
        return "Not Graded";

      default:
        return "Unknown";
    }
  }

}
