import {Component, Input, OnInit} from '@angular/core';
import {ModuleDetailItem} from "../../../../../../services/generated/models/module-detail-item";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../../../module/delete-dialog/delete-dialog.component";
import {lastValueFrom} from "rxjs";
import {ModuleService} from "../../../../../../services/generated/services/module.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ChapterService} from "../../../../../../services/generated/services/chapter.service";

@Component({
  selector: 'app-task-list-item',
  templateUrl: './task-list-item.component.html',
  styleUrls: ['./task-list-item.component.scss']
})
export class TaskListItemComponent implements OnInit {

  // should be TaskDetailItem
  @Input() exercise: ModuleDetailItem = { };
  private isDeleting: boolean = false;
  private showLoading: boolean = false;
  constructor(private dialog: MatDialog,
              private readonly chapterService:ChapterService,
              private readonly snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  deleteExerciseDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(DeleteDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        //if (data == "delete") this.deleteExercise();
      }
    );
  }
}
