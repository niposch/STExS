import {Component, OnInit, NgModule, Input, Output, EventEmitter} from '@angular/core';
import {Module} from "../../../services/generated/models/module";
import {ModuleService} from "../../../services/generated/services/module.service";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DeleteDialogComponent} from "./delete-dialog/delete-dialog.component";
import {ModuleDetailItem} from "../../../services/generated/models/module-detail-item";

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.scss']
})
export class ModuleComponent implements OnInit {

  @Input() isFavorited = false;
  @Output() isFavoritedEventEmitter = new EventEmitter<boolean>();
  @Output() onModuleDelete = new EventEmitter<any>();
  @Input() module:ModuleDetailItem | undefined;

  private isDeleting : boolean = false;


  constructor(private readonly moduleService:ModuleService,
              private readonly snackBar: MatSnackBar,
              private dialog: MatDialog
              ) { }

  ngOnInit(): void {
  }

  deleteModule() {
    if (this.isDeleting) {
      return;
    }

    this.isDeleting = true;

    lastValueFrom(this.moduleService.apiModuleModuleIdDelete({
      moduleId: this.module!.moduleId!
    }))
      .catch(err =>{
      this.snackBar.open("Could not delete Module", "Dismiss", {duration:5000})
      throw err
    }).then(() => {
      this.isDeleting = false;
      this.onModuleDelete.emit();
    })
  }

  openDialog() {
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

}
