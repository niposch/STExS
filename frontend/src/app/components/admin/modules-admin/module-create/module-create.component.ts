import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {lastValueFrom} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatSliderChange} from "@angular/material/slider";

@Component({
  selector: 'app-module-create',
  templateUrl: './module-create.component.html',
  styleUrls: ['./module-create.component.scss']
})
export class ModuleCreateComponent implements OnInit {

  public name:string = "";
  public description: string = "";
  public nrParticipants: number | null = 0;
  public nrParticipantsText: string = "0";

  @Output() public onModuleCreate: EventEmitter<any> = new EventEmitter<any>()
  public isLoading = false;
  public showLoading: boolean = false;
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

    this.showLoading = true;
    this.isLoading = true;

    console.log(this.description);

    lastValueFrom(this.moduleService.apiModulePost({
      body:{
        moduleDescription: this.description,
        moduleName: this.name,
        maxParticipants: Number(this.nrParticipants),
      }
    }))
      .catch(err =>{
          this.snackBar.open("Could not create Module", "Dismiss", {duration:5000});
          this.showLoading = false;
          this.isLoading = false;
          throw err
        }).then(() => {
          this.snackBar.open("Successfully created Module", "Dismiss", {duration:2000});
          this.isLoading = false;
          this.showLoading = false;
          this.onModuleCreate.emit()
          this.name = "";
          this.description = "";
    });
  }

  changePartNr($event: MatSliderChange) {
    let value = $event.value;
    this.nrParticipants = value;

    // @ts-ignore
    if (value > 200) {
      this.nrParticipantsText = "not limited"
      this.nrParticipants = null;
    } else {
      // @ts-ignore
      this.nrParticipantsText = this.nrParticipants.toString();
    }
  }
}
