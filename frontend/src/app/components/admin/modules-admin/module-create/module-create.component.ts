import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {catchError, firstValueFrom, flatMap, lastValueFrom, mergeMap, of, scan} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {Module} from "../../../../../services/generated/models/module";

@Component({
  selector: 'app-module-create',
  templateUrl: './module-create.component.html',
  styleUrls: ['./module-create.component.scss']
})
export class ModuleCreateComponent implements OnInit {

  public name:string = ""
  public description: string = ""

  @Output() public onModuleCreate: EventEmitter<any> = new EventEmitter<any>()
  public isLoading = false;
  constructor(private readonly moduleService:ModuleService,
              private readonly snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  createModule() {
    if (this.isLoading) {
      return;
    }
    if (this.description == "" && this.name == "") {
      this.snackBar.open('Please enter a name and a description!', 'ok');
      return;
    }

    this.isLoading = true;
    lastValueFrom(this.moduleService.apiModulePost({
      body:{
        moduleDescription: this.description,
        moduleName: this.name
      }
    }))
      .catch(err =>{
          this.snackBar.open("Could not create Module", "Dismiss", {duration:5000})
          throw err
        }).then(() => {
      this.isLoading = false;
      this.onModuleCreate.emit()
    });

    this.name = "";
    this.description = "";
  }
}
