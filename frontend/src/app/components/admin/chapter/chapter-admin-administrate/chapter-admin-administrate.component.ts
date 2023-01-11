import { Component, OnInit } from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ActivatedRoute, Router} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";


@Component({
  selector: 'app-chapter-admin-administrate',
  templateUrl: './chapter-admin-administrate.component.html',
  styleUrls: ['./chapter-admin-administrate.component.scss']
})

export class ChapterAdminAdministrateComponent implements OnInit {
  public moduleId : string | null | undefined = "";
  public chapterName : string | null | undefined = "";
  public chapterDescription : string | null | undefined = "";
  public isEditingName: boolean = false;
  public newChapterName: string = "";
  public moduleName: string | null | undefined = "";

  public savingInProgress = false;
  public showLoading = false;
  public isLoading: boolean = false;


  constructor(private readonly activatedRoute:ActivatedRoute,
    private readonly moduleService: ModuleService,
    private router: Router, public snackBar: MatSnackBar,) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["module_id"] != null){
        this.chapterName = params["chapter_name"];
        this.loadModule(params["module_id"]);
      }
    })
    this.isLoading = false;
  }

  loadModule(moduleId:string){
    this.moduleId = moduleId;
    this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    })
      .subscribe(m => {
        this.moduleName = m?.moduleName
      })
  }

  nameEditButton() {
    this.isEditingName = !this.isEditingName;

    if ( (!this.isEditingName) && (this.newChapterName != "") ) {
      this.chapterName = this.newChapterName;
    }
  }

  saveModuleChanges() {
    if (this.savingInProgress) return;

    this.showLoading = true;

    this.savingInProgress = true;
    //BACKEND API POST ROUTE to change chapter info
    this.snackBar.open("Successfully saved changes!", "Dismiss", {duration:2000})
    this.savingInProgress = false;
    this.showLoading = false;
  }

  linkToModule() {
    this.router.navigateByUrl("/module/administrate?id="+this.moduleId)
  }
}
