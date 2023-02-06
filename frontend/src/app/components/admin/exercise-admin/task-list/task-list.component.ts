import {AfterViewInit, Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {debounce, fromEvent, interval} from "rxjs";
import {map} from "rxjs/operators";
import {ChapterService} from "../../../../../services/generated/services/chapter.service";
import {ExerciseService} from "../../../../../services/generated/services/exercise.service";
import {ExerciseDetailItem} from "../../../../../services/generated/models/exercise-detail-item";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit, AfterViewInit {

  @Input() chapterId: string = "";
  @Output() exerciseChange = new EventEmitter<boolean>;
  public results: Array<ExerciseDetailItem> | null = null;
  public loading: boolean = false;
  @ViewChild("searchInput") searchInputRef: ElementRef | undefined
  public displayedColumns = ["exerciseName", "exerciseDescription", "actions"]
  public isLoading: boolean = false;

  constructor(private readonly exerciseService: ExerciseService,
              public snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.search("")
  }

  search(searchString:string){
    this.exerciseService.apiExerciseAllGet$Json()
      .subscribe(eList => {
      this.results = eList;
    })

    /*
    //TODO searching
    this.exerciseService.apiExerciseByChapterIdGet$Json({
      search:searchString
    }).subscribe(data => {
      this.results = data
    })
     */
  }

  ngAfterViewInit(): void {
    // @ts-ignore
    fromEvent<Event>(this.searchInputRef.nativeElement, "keyup")
      .pipe(
        debounce(() => interval(200)),
        // @ts-ignore
        map(v => v?.target?.value ?? "")
      )
      .subscribe(searchString => {
        console.log(searchString)
        this.search(searchString)
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
}
