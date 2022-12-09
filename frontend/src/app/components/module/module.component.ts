import { Component, OnInit, NgModule, Input } from '@angular/core';

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.scss']
})
export class ModuleComponent implements OnInit {
  @Input() name = ''; // decorate the property with @Input()
  @Input() add_module = ''; // decorate the property with @Input()
  @Input() checked = ''; // decorate the property with @Input()
  
  constructor() { }

  ngOnInit(): void {
  }
  
  checkValue(event: any){
   console.log(event);
  }

}
