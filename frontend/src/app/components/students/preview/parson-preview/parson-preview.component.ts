import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-parson-preview',
  templateUrl: './parson-preview.component.html',
  styleUrls: ['./parson-preview.component.scss']
})
export class ParsonPreviewComponent implements OnInit {

  @Input() exerciseId!: string;
  @Input() submissionId: string|null = null;
  constructor() { }

  ngOnInit(): void {
  }

}
