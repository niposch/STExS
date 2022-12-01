import { Component, OnInit, NgModule, Input } from '@angular/core';

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.scss']
})
export class ModuleComponent implements OnInit {
  @Input() name = ''; // decorate the property with @Input()
  @Input() isAdmin = ''; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }

}
