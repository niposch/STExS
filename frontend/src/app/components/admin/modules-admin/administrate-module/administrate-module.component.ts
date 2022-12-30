import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ModuleDetailItem} from "../../../../../services/generated/models/module-detail-item";

@Component({
  selector: 'app-administrate-module',
  templateUrl: './administrate-module.component.html',
  styleUrls: ['./administrate-module.component.scss']
})
export class AdministrateModuleComponent implements OnInit {

  public module: ModuleDetailItem|null = null;
  constructor(private readonly activatedRoute:ActivatedRoute,
              private readonly moduleService: ModuleService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["id"] != null){
        this.loadModule(params["id"])
      }
    })
  }

  loadModule(id:string){
    this.moduleService.apiModuleGetByIdGet$Json({
      id: id
    })
      .subscribe(m => this.module = m)
  }
}
