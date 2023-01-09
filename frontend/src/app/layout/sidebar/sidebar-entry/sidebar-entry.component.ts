import {Component, Input, OnInit} from '@angular/core';
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";

@Component({
  selector: 'app-sidebar-entry',
  templateUrl: './sidebar-entry.component.html',
  styleUrls: ['./sidebar-entry.component.scss']
})
export class SidebarEntryComponent implements OnInit {

  // @ts-ignore
  @Input() module : ModuleDetailItem;

  constructor() { }

  ngOnInit(): void {
  }

}
