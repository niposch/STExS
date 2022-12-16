import {Component, Input} from '@angular/core';

@Component({
  selector: 'app-user-info-label',
  templateUrl: './user-info-label.component.html',
  styleUrls: ['./user-info-label.component.scss']
})
export class UserInfoLabelComponent {

  @Input() attributeName: string = "null";
  @Input() attributeInfo: string = "null";

  constructor() { }

}
