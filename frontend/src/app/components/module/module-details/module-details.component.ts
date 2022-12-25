import { Component, OnInit } from '@angular/core';
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ActivatedRoute} from "@angular/router";
import {catchError, firstValueFrom, lastValueFrom} from "rxjs";

@Component({
  selector: 'app-module-details',
  templateUrl: './module-details.component.html',
  styleUrls: ['./module-details.component.scss']
})
export class ModuleDetailsComponent implements OnInit {
  public module:ModuleDetailItem | null | undefined = undefined;
  constructor(private readonly moduleService:ModuleService,
              private readonly activatedRoute:ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params =>{
      if(params["id"] == null) {
        this.module = null
        return;
      }
        firstValueFrom(this.moduleService.apiModuleGetByIdGet$Json({
          moduleId: params["moduleId"]
        }))
          .catch(err => {
            this.module = null;
            throw err;
          })
          .then(data => this.module = data)
    })
  }

}
