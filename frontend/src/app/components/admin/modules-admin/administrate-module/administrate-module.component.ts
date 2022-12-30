import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ModuleDetailItem} from "../../../../../services/generated/models/module-detail-item";
import {lastValueFrom} from "rxjs";

@Component({
  selector: 'app-administrate-module',
  templateUrl: './administrate-module.component.html',
  styleUrls: ['./administrate-module.component.scss']
})
export class AdministrateModuleComponent implements OnInit {

  public module: ModuleDetailItem | null = null;
  public savingInProgress: boolean = false;
  public isEditingName: boolean = false;

  public mId : string = "";

  public moduleName: string | null | undefined = "";
  public newModuleName: string = "";

  public moduleDescription: string | null | undefined = "";
  public newModuleDescription: string = "";

  public maxPart : number | null | undefined = 0;
  public newMaxPart : number | null | undefined = 0;

  constructor(private readonly activatedRoute:ActivatedRoute,
              private readonly moduleService: ModuleService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["id"] != null){
        this.loadModule(params["id"])
      }
    })
  }

  loadModule(moduleId:string){
    this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    })
      .subscribe(m => {
        this.module = m;

        this.moduleName = this.module?.moduleName
        this.moduleDescription = this.module.moduleDescription;
        this.maxPart = this.module.maxParticipants;
        this.mId = moduleId;
      })
  }

  editButtonClick() {
    this.isEditingName = !this.isEditingName;

    if (this.newModuleName == "") {
      // @ts-ignore
      this.newModuleName = this.module?.moduleName;
    }

    if (!this.isEditingName) {
      this.moduleName = this.newModuleName;
    }
  }

  async saveModuleChanges() {
    if (this.savingInProgress) return;

    this.savingInProgress = true;
    //BACKEND API POST ROUTE to change user info
    await lastValueFrom(this.moduleService.apiModuleModuleIdPut({
      moduleId: this.mId,
      body: {
        moduleName: this.moduleName,
        moduleDescription: this.moduleDescription,
        maxParticipants: this.maxPart
      }
    }));
    await lastValueFrom(this.moduleService.apiModuleGetUsersForModuleGet$Json())
    this.savingInProgress = false;
  }
}
