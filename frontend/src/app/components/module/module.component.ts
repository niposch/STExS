import {Component, OnInit, NgModule, Input, Output, EventEmitter} from '@angular/core';
import {Module} from "../../../services/generated/models/module";

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.scss']
})
export class ModuleComponent implements OnInit {

  @Input() isFavorited = false;
  @Output() isFavoritedEventEmitter = new EventEmitter<boolean>();
  @Input() module:Module | undefined;

  constructor() { }

  ngOnInit(): void {
  }
}
