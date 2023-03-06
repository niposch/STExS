import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-create-exercise',
  templateUrl: './create-exercise.component.html',
  styleUrls: ['./create-exercise.component.scss']
})
export class CreateExerciseComponent implements OnInit {

  @Input() public description = "";
  @Input() public name = "";
  @Input() isOnlyInspecting = false;
  @Output() descriptionChange = new EventEmitter<string>();
  @Output() nameChange = new EventEmitter<string>();
  ngOnInit(): void {}

  emitValues() {
    this.descriptionChange.emit(this.description ?? "")
    this.nameChange.emit(this.name ?? "")
  }

}
