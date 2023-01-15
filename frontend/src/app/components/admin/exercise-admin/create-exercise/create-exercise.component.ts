import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-create-exercise',
  templateUrl: './create-exercise.component.html',
  styleUrls: ['./create-exercise.component.scss']
})
export class CreateExerciseComponent implements OnInit {

  public description = "";
  public name = "";
  @Output() descEvent = new EventEmitter<string>();
  @Output() nameEvent = new EventEmitter<string>();
  ngOnInit(): void {}

  emitValues() {
    this.descEvent.emit(this.description)
    this.nameEvent.emit(this.name)
  }

}
