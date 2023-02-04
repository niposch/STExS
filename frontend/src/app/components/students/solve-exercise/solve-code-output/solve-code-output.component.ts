import {Component, Input, OnInit} from '@angular/core';
import {lastValueFrom} from "rxjs";
import {CodeOutputService} from "../../../../../services/generated/services/code-output.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {CodeOutputDetailItem} from "../../../../../services/generated/models/code-output-detail-item";

@Component({
  selector: 'app-solve-code-output',
  templateUrl: './solve-code-output.component.html',
  styleUrls: ['./solve-code-output.component.scss']
})
export class SolveCodeOutputComponent implements OnInit {

  @Input() id: string = "";
  public exercise : CodeOutputDetailItem | null = {};
  public isLoading : boolean = false;

  constructor(private readonly codeoutputService: CodeOutputService,
              public snackBar: MatSnackBar,) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.loadExercise();
  }

  loadExercise() {
    lastValueFrom(this.codeoutputService.apiCodeOutputGet$Json({
      id: this.id
    })).catch(() => {
        this.snackBar.open("Could not load this Code-Output Exercise", "ok", {duration: 3000});
      }
    ).then(data => {
      // @ts-ignore
      this.exercise = data;
      this.isLoading = false;
    })
  }

}
