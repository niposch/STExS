import { Component, OnInit, Input, Output } from '@angular/core';

@Component({
  selector: 'app-view-cloze',
  templateUrl: './view-cloze.component.html',
  styleUrls: ['./view-cloze.component.scss']
})
export class ViewClozeComponent implements OnInit {

  @Input() text: string = "";
  @Input() enableInputfield: boolean = true;
  public splitted: Array <string> | null=null;
  public gaps: Array <string> | null=null;

  constructor() { }

  ngOnInit(): void {
    this.splitText();
    this.initGaps();
  }

splitText() {
    const regex = /\[\[[^\[]*\]\]/;
    this.splitted = this.text.split(regex);
  }

  initGaps() {
    if(this.splitted) this.gaps = new Array <string>(this.splitted.length - 1);
  }
}

