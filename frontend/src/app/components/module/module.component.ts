import {Component, OnInit, NgModule, Input, Output, EventEmitter} from '@angular/core';
import {Module} from "../../../services/generated/models/module";
import {ModuleService} from "../../../services/generated/services/module.service";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {HttpContext} from "@angular/common/http";
import {MatDialog, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.scss']
})
export class ModuleComponent implements OnInit {

  @Input() isFavorited = false;
  @Output() isFavoritedEventEmitter = new EventEmitter<boolean>();
  @Output() onModuleDelete = new EventEmitter<any>();
  @Input() module:Module | undefined;

  private isDeleting : boolean = false;

  constructor(private readonly moduleService:ModuleService,
              private readonly snackBar: MatSnackBar,
              private readonly dialog:MatDialog) { }

  ngOnInit(): void {
  }

  deleteModule() {
    if (this.isDeleting) {
      return;
    }

    this.isDeleting = true;

    lastValueFrom(this.moduleService.apiModuleDelete({
      moduleId: this.module?.moduleId
    }))
      .catch(err =>{
      this.snackBar.open("Could not delete Module", "Dismiss", {duration:5000})
      throw err
    }).then(() => {
      this.isDeleting = false;
      this.onModuleDelete.emit();
    })
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(DialogAnimationsExampleDialog, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
}

export class DialogAnimationsExampleDialog {
  constructor(public dialogRef: MatDialogRef<DialogAnimationsExampleDialog>) {}
}
