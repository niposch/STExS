import {Component, OnInit} from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ActivatedRoute, Router} from "@angular/router";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../../module/delete-dialog/delete-dialog.component";
import {ChapterService} from "../../../../../services/generated/services/chapter.service";
import {ChapterDetailItem} from "../../../../../services/generated/models/chapter-detail-item";
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";
import {ExerciseType} from "../../../../../services/generated/models/exercise-type";

@Component({
  selector: 'app-chapter-admin-administrate',
  templateUrl: './chapter-admin-administrate.component.html',
  styleUrls: ['./chapter-admin-administrate.component.scss']
})

export class ChapterAdminAdministrateComponent implements OnInit {
  public chapter : ChapterDetailItem | null = null;
  public chapterName : string | null | undefined = "";
  public chapterDescription : string | null | undefined = "";
  public moduleName: string | null | undefined = "Module";
  public isEditingName: boolean = false;
  public newChapterName: string = "";
  public savingInProgress = false;
  public showLoading = false;
  public isLoading: boolean = true;
  private isDeleting: boolean = false;
  public chapterId: string = "";
  public exerciseList: Array<ExerciseDetailItem> | null = null;
  public hasExercises: boolean = false;
  public exerciseType: number = -1;

  public ExerciseTypeEnum = ExerciseType
  private moduleId : string = "";

  constructor(private readonly activatedRoute:ActivatedRoute,
              private router: Router, public snackBar: MatSnackBar,
              private readonly chapterService:ChapterService,
              private readonly moduleService:ModuleService,
              private readonly exerciseService: ExerciseService,
              private dialog: MatDialog) { }

  ngOnInit() {
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["chapterId"] != null){
        this.chapterId = params["chapterId"];
        this.loadChapter(this.chapterId);
      }
    })
  }

  loadChapter(id:string){
    this.chapterService.apiChapterGet$Json({
      chapterId: id
    })
      .subscribe(c => {
        this.chapter = c;
        this.chapterName = c?.chapterName;
        this.chapterDescription = c?.chapterDescription;
        // @ts-ignore
        this.moduleId = c?.moduleId;

        this.loadModule(this.moduleId)
        this.loadExercises();
      })
  }

  loadModule (id : string) {
    console.log("ID: " + id)
    this.moduleService.apiModuleGetByIdGet$Json({
      id: id
    })
      .subscribe(m => {
        this.moduleName = m.moduleName;
      })
  }

  loadExercises() {
    this.exerciseService.apiExerciseByChapterIdGet$Json({
      chapterId: this.chapterId
    })
      .subscribe(e => {
        this.exerciseList = e;
        this.hasExercises = this.exerciseList.length > 0;
        this.isLoading = false;
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
    this.snackBar.open("Could not save changes!", "understood", {duration:2000})
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

  createExercise() {
    if (this.exerciseType == -1) {
      this.snackBar.open("Please select an Exercise Type", "understood");
      return;
    }

    if (this.exerciseType == ExerciseType.CodeOutput) {
      this.router.navigate(
        ['codeoutput/create'],
        {queryParams: {chapterId: this.chapterId}}
      );
      return;
    } else {
      this.router.navigate(['**'])
    }

  }
}
