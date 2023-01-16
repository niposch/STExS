import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ModuleDetailItem} from "../../../../../services/generated/models/module-detail-item";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {DeleteDialogComponent} from "../../../module/delete-dialog/delete-dialog.component";
import {lastValueFrom} from "rxjs";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ChapterService} from "../../../../../services/generated/services/chapter.service";
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";
import {ExerciseType} from "../../../../../services/generated/models/exercise-type";

@Component({
  selector: 'app-task-list-item',
  templateUrl: './task-list-item.component.html',
  styleUrls: ['./task-list-item.component.scss']
})
export class TaskListItemComponent implements OnInit {

  // should be TaskDetailItem
  @Input() exercise: ExerciseDetailItem = { };
  @Output() exerciseChange = new EventEmitter<boolean>;
  public exerciseType = ExerciseType;
  private isDeleting: boolean = false;
  private showLoading: boolean = false;
  constructor(private dialog: MatDialog,
              private readonly exerciseService:ExerciseService,
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
        if (data == "delete") this.deleteExercise();
      }
    );
  }

  deleteExercise() {
    this.exerciseService.apiExerciseDelete({
      exerciseId: this.exercise.id
    }).subscribe( () => {
      this.exerciseChange.emit(true);
    })
  }
}
