import {AfterViewInit, Component, Input, OnInit} from '@angular/core';
import {ModuleService} from "../../../../services/generated/services/module.service";
import {ModuleDetailItem} from "../../../../services/generated/models/module-detail-item";

@Component({
  selector: 'app-join-module',
  templateUrl: './join-module.component.html',
  styleUrls: ['./join-module.component.scss']
})
export class JoinModuleComponent implements OnInit, AfterViewInit{
  searchInput : string = "";
  @Input() infoText : string = "";
  public results: Array<ModuleDetailItem> | null = null;
  public displayedColumns = ["moduleName", "teacher", "actions"]
  constructor(private readonly moduleService:ModuleService) { }

  ngOnInit(): void {
    this.search("")
  }

  search(searchString:string){
    this.moduleService.apiModuleSearchGet$Json({
      search:searchString
    }).subscribe(data => {
      this.results = data
    })


  }

  ngAfterViewInit(): void {
    /*
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
     */
  }

}
