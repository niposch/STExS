import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ActivatedRoute, Router} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../../module/delete-dialog/delete-dialog.component";
import {ChapterService} from "../../../../../services/generated/services/chapter.service";
import {ChapterDetailItem} from "../../../../../services/generated/models/chapter-detail-item";
import {ModuleDetailItem} from "../../../../../services/generated/models/module-detail-item";


@Component({
  selector: 'app-chapter-admin-administrate',
  templateUrl: './chapter-admin-administrate.component.html',
  styleUrls: ['./chapter-admin-administrate.component.scss']
})

export class ChapterAdminAdministrateComponent implements OnInit {
  public moduleId : string | null | undefined = "";

  public module : ModuleDetailItem | null = null;
  public chapter : ChapterDetailItem | null = null;
  public chapterName : string | null | undefined = "chapter_name";
  public chapterDescription : string | null | undefined = "chapter_description";

  public isEditingName: boolean = false;
  public newChapterName: string = "";
  public moduleName: string | null | undefined = "";

  public savingInProgress = false;
  public showLoading = false;
  public isLoading: boolean = false;
  private isDeleting: boolean = false;

  @Output() onChapterChange = new EventEmitter<any>();
  private chapterId: string | null = "";


  constructor(private readonly activatedRoute:ActivatedRoute,
              private router: Router, public snackBar: MatSnackBar,
              private readonly chapterService:ChapterService,
              private readonly moduleService:ModuleService,
              private dialog: MatDialog) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["module_id"] != null){
        this.chapterId = params["chapterId"];
      }
    })
    this.isLoading = false;
    this.chapterName = this.chapter?.chapterName;
  }

  loadModule(moduleId:string){
    this.moduleId = moduleId;
    this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    })
      .subscribe(m => {
        this.moduleName = m?.moduleName
        this.module = m;
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

  deleteChapterDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data == "delete") this.deleteChapter();
      }
    );
  }

  private deleteChapter() {
    if (this.isDeleting) {
      return;
    }

    this.showLoading = true;
    this.isDeleting = true;

    lastValueFrom(this.chapterService.apiChapterDelete({
      //chapterId: this.chapter?.runningNumber
    }))
      .catch(err =>{
        this.snackBar.open("Could not delete Module", "Dismiss", {duration:5000});
        this.isDeleting = false;
        this.showLoading = false;
        throw err
      }).then(() => {
      this.snackBar.open("Successfully deleted Module", "Dismiss", {duration:2000});
      this.isDeleting = false;
      this.showLoading = false;
      this.onChapterChange.emit();
    })
  }
}
