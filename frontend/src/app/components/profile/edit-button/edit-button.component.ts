import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-edit-button',
  templateUrl: './edit-button.component.html',
  styleUrls: ['./edit-button.component.scss']
})
export class EditButtonComponent implements OnInit {
  //@Input() isEditing: boolean = true;

  isEditing: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

}
