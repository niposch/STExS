import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-modules-admin',
  templateUrl: './modules-admin.component.html',
  styleUrls: ['./modules-admin.component.scss']
})
export class ModulesAdminComponent implements OnInit {
  module_1 = "Kurs 1";
  module_2 = "Kurs 2";
  module_3 = "Kurs 3";
  constructor() { }

  ngOnInit(): void {
  }

}
