import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-create-edit-cloze',
  templateUrl: './create-edit-cloze.component.html',
  styleUrls: ['./create-edit-cloze.component.scss']
})
export class CreateEditClozeComponent implements OnInit {

  @Input()
  public exerciseId: string|null = null;
  constructor(
    private readonly activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if (params["exerciseId"] != null) {
        this.exerciseId = params["exerciseId"];
      }
    })
  }

  async loadExercise(exerciseId: string):Promise {

  goBack() {
    window.history.back();
  }
}
