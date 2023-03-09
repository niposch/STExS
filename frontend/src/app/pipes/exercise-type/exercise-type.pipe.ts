import { Pipe, PipeTransform } from '@angular/core';
import {ExerciseType} from "../../../services/generated/models/exercise-type";

@Pipe({
  name: 'exerciseType'
})
export class ExerciseTypePipe implements PipeTransform {

  transform(value: ExerciseType|null|undefined, ...args: unknown[]): string {
    if(value == null) return "Unknown";
    switch (value) {
      case ExerciseType.ClozeText:
        return "Cloze Text";
      case ExerciseType.Parson:
        return "Parson";
      case ExerciseType.CodeOutput:
        return "Code Output";
      default:
        return "Unknown";
    }
  }
}
