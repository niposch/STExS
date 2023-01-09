import {Component, OnInit, NgModule, Input, Output, EventEmitter} from '@angular/core';
import {Module} from "../../../services/generated/models/module";
import {ModuleService} from "../../../services/generated/services/module.service";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DeleteDialogComponent} from "./delete-dialog/delete-dialog.component";
import {ModuleDetailItem} from "../../../services/generated/models/module-detail-item";
import {ArchiveDialogComponent} from "./archive-dialog/archive-dialog.component";

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.scss']
})
export class ModuleComponent implements OnInit {

  @Input() isFavorited = false;
  @Output() isFavoritedEventEmitter = new EventEmitter<boolean>();
  @Output() onModuleChange = new EventEmitter<any>();
  @Input()
  isModuleAdmin = false;
  @Input() module:ModuleDetailItem | undefined;

  private isDeleting : boolean = false;
  private isArchiving: boolean = false;
  public showLoading: boolean = false;


  constructor(private readonly moduleService:ModuleService,
              private readonly snackBar: MatSnackBar,
              private dialog: MatDialog
              ) { }

  ngOnInit(): void {
  }

  openDeleteDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data == "delete") this.deleteModule();
      }
    );
  }

  deleteModule() {
    if (this.isDeleting) {
      return;
    }

    this.showLoading = true;
    this.isDeleting = true;

    lastValueFrom(this.moduleService.apiModuleModuleIdDelete({
      moduleId: this.module!.moduleId!
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
      this.onModuleChange.emit();
    })
  }

  handleArchiving() {
    if (this.module?.isArchived) {
      this.unArchiveModule();
    } else {
      this.openArchiveDialog();
    }
  }

  private openArchiveDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    const dialogRef = this.dialog.open(ArchiveDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data == "archive") this.archiveModule();
      }
    );
  }

  private archiveModule() {
    if (this.isArchiving) {
      return;
    }

    this.showLoading = true;
    this.isArchiving = true;

    lastValueFrom(this.moduleService.apiModuleArchivePost({
      moduleId: this.module!.moduleId!
    }))
      .catch(err =>{
        this.snackBar.open("Could not archive Module", "Dismiss", {duration:5000});
        this.isArchiving = false;
        this.showLoading = false;
        throw err
      }).then(() => {
      this.snackBar.open("Successfully archived Module", "Dismiss", {duration:2000});
      this.isArchiving = false;
      this.showLoading = false;
      this.onModuleChange.emit();
    })
  }

  private unArchiveModule() {
    if (this.isArchiving) {
      return;
    }

    this.showLoading = true;
    this.isArchiving = true;

    lastValueFrom(this.moduleService.apiModuleUnarchivePost({
      moduleId: this.module!.moduleId!
    }))
      .catch(err =>{
        this.snackBar.open("Could not unarchive Module", "Dismiss", {duration:5000});
        this.isArchiving = false;
        this.showLoading = false;
        throw err
      }).then(() => {
      this.snackBar.open("Successfully unarchived Module", "Dismiss", {duration:2000});
      this.isArchiving = false;
      this.showLoading = false;
      this.onModuleChange.emit();
    })
  }

  /*
  favoriteModule() {
    if (!this.module?.isFavorited) {
      //favorite this module
      lastValueFrom(this.moduleService.apiModuleModuleIdPut({
        //fav this module
      }))
    } else {
      //un-favorite this module
    }
  }

   */
}
