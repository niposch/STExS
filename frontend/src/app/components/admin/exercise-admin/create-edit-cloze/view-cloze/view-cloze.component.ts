import { Component, OnInit,OnChanges, Input, ViewChild } from '@angular/core';

@Component({
  selector: 'app-view-cloze',
  templateUrl: './view-cloze.component.html',
  styleUrls: ['./view-cloze.component.scss']
})
export class ViewClozeComponent implements OnInit, OnChanges {

  @Input() text: string | undefined| null = null;
  @Input() enableInputfield: boolean = true;
  @Input() userHasSolvedExercise: undefined | null | boolean = false;
  @Input() iGaps: Array <string> | undefined | null = null;
  public gaps: Array <string> | undefined | null = null;
  public splitted: Array <string> | undefined | null;


  constructor() { }

  ngOnInit(): void {
    this.splitText();
    this.updateGaps();
  }

  ngOnChanges(): void {
    this.splitText();
    this.updateGaps();
  }

  splitText() {
    const regex = /\[\[[^\[]*\]\]/;
    if(this.text) this.splitted = this.text.split(regex);
  }

  updateGaps() {
    if(this.splitted) {
      if(!this.iGaps) this.gaps = new Array <string>(this.splitted.length - 1);   //Preview
      else this.gaps = this.iGaps;                                                //Solve
    }
  }
}

