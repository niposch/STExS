import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-modules-user',
  templateUrl: './modules-user.component.html',
  styleUrls: ['./modules-user.component.scss']
})
export class ModulesUserComponent implements OnInit {
  module_1 = "Kurs 1";
  module_2 = "Kurs 2";
  module_3 = "Kurs 3";
  constructor() { }

  ngOnInit(): void {
  }

}
