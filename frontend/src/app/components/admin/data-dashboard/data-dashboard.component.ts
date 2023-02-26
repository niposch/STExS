import { Component, OnInit } from '@angular/core';
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ActivatedRoute} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";

@Component({
  selector: 'app-data-dashboard',
  templateUrl: './data-dashboard.component.html',
  styleUrls: ['./data-dashboard.component.scss']
})
export class DataDashboardComponent implements OnInit {

  public moduleInfo: ModuleDetailItem | undefined;
  public isLoading: boolean = false;
  constructor(private readonly moduleService : ModuleService,
              private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.isLoading = true;
    let moduleId : string = "";
    this.route.queryParams.subscribe(params => {
      moduleId = params['moduleId'];
      this.loadModuleInfo(moduleId).then(() => {
        this.isLoading = false;
      })
    })
  }

  async loadModuleInfo(moduleId:string) {
    await lastValueFrom(this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    })).then((data) => {
      if (data == null) return;
      this.moduleInfo = data;
    }).catch((error) => {
      console.log(error);
    });
  }
}
