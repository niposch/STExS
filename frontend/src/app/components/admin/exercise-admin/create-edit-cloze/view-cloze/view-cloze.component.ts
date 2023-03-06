import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-view-cloze',
  templateUrl: './view-cloze.component.html',
  styleUrls: ['./view-cloze.component.scss']
})
export class ViewClozeComponent implements OnInit {

  @Input() text: string | null | undefined;
  @Input() enableInputfield: boolean = true;
  @Input() userHasSolvedExercise: undefined | null | boolean = false;
  public splitted: Array <string> | undefined | null;
  public gaps: Array <string> | undefined | null;


  constructor() { }

  ngOnInit(): void {
    this.splitText();
    this.initGaps();
  }

  ngOnChanges(): void {
    this.splitText();
    this.initGaps();
  }

splitText() {
    const regex = /\[\[[^\[]*\]\]/;
    if(this.text) this.splitted = this.text.split(regex);
  }

  initGaps() {
    if(this.splitted) this.gaps = new Array <string>(this.splitted.length - 1);
  }
}

