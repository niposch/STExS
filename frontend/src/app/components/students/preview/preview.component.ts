import {Component, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {ExerciseType} from "../../../../services/generated/models/exercise-type";

@Component({
  selector: 'app-preview',
  templateUrl: './preview.component.html',
  styleUrls: ['./preview.component.scss']
})
export class PreviewComponent implements OnInit, OnChanges {

  @Input() exerciseId!: string;
  @Input() submissionId: string|null = null;
  @Input() exerciseType!: ExerciseType;

  public ExerciseType = ExerciseType;

  constructor() { }

  ngOnInit(): void {
  }
  ngOnChanges(changes: SimpleChanges) {
    if(changes['exerciseId']?.currentValue != null){
      this.exerciseId = changes['exerciseId']?.currentValue;
    }
    this.submissionId = changes['submissionId']?.currentValue;
    if(changes['exerciseType']?.currentValue != null){
      this.exerciseType = changes['exerciseType']?.currentValue;
    }
  }
}
