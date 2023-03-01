import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Router} from "@angular/router";
import {ExerciseType} from "../../../../../services/generated/models/exercise-type";

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {

  @Input() chapterId: string = "";
  @Output() exerciseChange = new EventEmitter<boolean>;
  public results: Array<ExerciseDetailItem> | null = null;
  public loading: boolean = false;
  public searchInput: string = "";
  public displayedColumns = ["exerciseName", "exerciseDescription", "actions"]
  public isLoading: boolean = false;

  constructor(private readonly exerciseService: ExerciseService,
              public snackBar: MatSnackBar,
              public router: Router) { }

  ngOnInit(): void {
    this.search("")
  }

  search(searchString:string){
    this.isLoading = true;
    this.exerciseService.apiExerciseSearchGet$Json({
      search:searchString
    })
      .subscribe(eList => {
      this.results = eList;
      if (this.results.length === 0) {
        this.snackBar.open('No Exercises found', 'ok', {duration:3000});
      }
      this.isLoading = false;
    })
  }

  addExercise(exerciseId: string) {
    this.isLoading = true;
    this.exerciseService.apiExerciseCopyExercisePost$Json({
      exerciseId: exerciseId,
      toChapter: this.chapterId
    }).subscribe(() => {
      this.exerciseChange?.emit(true);
      this.isLoading = false;
      this.snackBar.open('Successfully added Exercise', 'ok', {duration:3000})
    })
  }

  inspectExercise(exercise : ExerciseDetailItem) {
    if (exercise.exerciseType == ExerciseType.CodeOutput) {
      this.router.navigate(['codeoutput/create'], {
        queryParams: {
          exerciseId: exercise.id,
          inspecting: true
        }
      })
    } else if (exercise.exerciseType == ExerciseType.Parson) {
      this.router.navigate(['parson/create'], {
        queryParams: {
          exerciseId: exercise.id,
          inspecting: true
        }
      })
    }
  }
}
