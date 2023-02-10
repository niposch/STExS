import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-solve-gap-text',
  templateUrl: './solve-gap-text.component.html',
  styleUrls: ['./solve-gap-text.component.scss']
})
export class SolveGapTextComponent implements OnInit {

  @Input() id: string = "";
  private text: string = "";
  public splitted: Array <string> | null=null;
  public gaps: Array <string> | null=null;
  public showGaps: boolean = false;

  public isLoading : boolean = false;


  constructor() { }

  ngOnInit(): void {
    this.showGaps = false;
    this.isLoading = true;
    this.loadExercise();
    this.splitText();
    this.initGaps();

  }

  loadExercise() {
    this.text = "This is just a [[]]. If this is just a [[]], then the solution is always [[]]. And this text is very looooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong and has a lot of words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words words [[]].";
    this.isLoading = false;
  }

  splitText() {
    this.splitted = this.text.split("[[]]");
  }

  initGaps() {
    if(this.splitted) this.gaps = new Array <string>(this.splitted.length - 1);
  }

  buttonClick() {
    this.showGaps = !this.showGaps;
  }

}
