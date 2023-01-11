import {AfterViewInit, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {debounce, fromEvent, interval} from "rxjs";
import {map} from "rxjs/operators";

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit, AfterViewInit {

  public results: Array<{ }> | null = null;
  //results: Array<TaskDetailItem> | null = null;
  public loading: boolean = false;
  @ViewChild("searchInput") searchInputRef: ElementRef | undefined

  constructor() { }

  ngOnInit(): void {
    this.search("")
  }

  search(searchString:string){
    //api route to search through all tasks
    /*
    this.moduleService.apiModuleSearchGet$Json({
      search:searchString
    }).subscribe(data => {
      this.results = data
    })
    */
  }

  addTask(taskId: string) {
    //add the task to this chapter
  }

  ngAfterViewInit(): void {
    // @ts-ignore
    fromEvent<Event>(this.searchInputRef.nativeElement, "keyup")
      .pipe(
        debounce(v => interval(200)),
        // @ts-ignore
        map(v => v?.target?.value ?? "")
      )
      .subscribe(searchString => {
        console.log(searchString)
        this.search(searchString)
      })
  }
}
