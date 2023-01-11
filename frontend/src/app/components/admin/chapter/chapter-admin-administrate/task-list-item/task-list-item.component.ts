import {Component, Input, OnInit} from '@angular/core';
import {ChapterDetailItem} from "../../../../../../services/generated/models/chapter-detail-item";
import {ModuleDetailItem} from "../../../../../../services/generated/models/module-detail-item";

@Component({
  selector: 'app-task-list-item',
  templateUrl: './task-list-item.component.html',
  styleUrls: ['./task-list-item.component.scss']
})
export class TaskListItemComponent implements OnInit {

  // should be TaskDetailItem
  @Input() taskItem: ModuleDetailItem = { };
  constructor() { }

  ngOnInit(): void {
  }

}
