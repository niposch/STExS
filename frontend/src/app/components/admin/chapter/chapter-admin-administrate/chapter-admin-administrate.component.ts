import { Component, OnInit } from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ActivatedRoute, Router} from "@angular/router";


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


  constructor(private readonly activatedRoute:ActivatedRoute,
    private readonly moduleService: ModuleService,
    private router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["module_id"] != null){
        this.chapterName = params["chapter_name"];
        this.loadModule(params["module_id"]);
      }
    })
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

}
