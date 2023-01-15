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
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";


@Component({
  selector: 'app-chapter-admin-administrate',
  templateUrl: './chapter-admin-administrate.component.html',
  styleUrls: ['./chapter-admin-administrate.component.scss']
})

export class ChapterAdminAdministrateComponent implements OnInit {
  public chapter : ChapterDetailItem | null = null;
  public chapterName : string | null | undefined = "chapter_name";
  public chapterDescription : string | null | undefined = "chapter_description";
  public moduleName: string | null | undefined = "Module";
  public isEditingName: boolean = false;
  public newChapterName: string = "";
  public savingInProgress = false;
  public showLoading = false;
  public isLoading: boolean = false;
  private isDeleting: boolean = false;
  private chapterId: string = "";
  public exerciseList: Array<ExerciseDetailItem> | null = null;
  public hasExercises: boolean = false;

  constructor(private readonly activatedRoute:ActivatedRoute,
              private router: Router, public snackBar: MatSnackBar,
              private readonly chapterService:ChapterService,
              private readonly moduleService:ModuleService,
              private readonly exerciseService: ExerciseService,
              private dialog: MatDialog) { }

  ngOnInit(): void {
    this.isLoading = true;
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["chapterId"] != null){
        this.chapterId = params["chapterId"];
        this.loadChapter(this.chapterId);
        this.loadExercises();
      }
    })
    this.isLoading = false;
  }

  loadChapter(id:string){
    this.chapterService.apiChapterGet$Json({
      chapterId: id
    })
      .subscribe(c => {
        this.chapter = c;
        this.chapterName = c?.chapterName;
        this.chapterDescription = c?.chapterDescription;
      })

    this.moduleService.apiModuleGetByIdGet$Json({
      id: this.chapter?.moduleId
    })
      .subscribe(m => {
        this.moduleName = m.moduleName;
        console.log("MODULE NAME: " + this.moduleName)
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
    this.router.navigateByUrl("/module/administrate?id="+this.chapter?.moduleId)
  }

  deleteChapterDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(
      data => {
        if (data == "delete") this.deleteChapter();
        this.linkToModule();
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
      chapterId: this.chapter?.id
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
    })
  }

  private loadExercises() {
    this.exerciseService.apiExerciseByChapterIdGet$Json({
      chapterId: this.chapterId
    })
      .subscribe(e => {
        this.exerciseList = e;
        if (this.exerciseList.length > 0) {
          this.hasExercises = true;
        } else {
          this.hasExercises = false;
        }
      })
  }
}
