import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-cloze-text-preview',
  templateUrl: './cloze-text-preview.component.html',
  styleUrls: ['./cloze-text-preview.component.scss']
})
export class ClozeTextPreviewComponent implements OnInit {

  @Input() exerciseId!: string;
  @Input() submissionId: string|null = null;
  constructor() { }

  ngOnInit(): void {
  }

}
