import { Component, OnInit } from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {ModuleService} from "../../../../../services/generated/services/module.service";
import {ModuleDetailItem} from "../../../../../services/generated/models/module-detail-item";
import {lastValueFrom} from "rxjs";
import {DeleteDialogComponent} from "../../../module/delete-dialog/delete-dialog.component";
import {ArchiveDialogComponent} from "../../../module/archive-dialog/archive-dialog.component";
import {MatSnackBar} from "@angular/material/snack-bar";
import {MatDialog, MatDialogConfig} from "@angular/material/dialog";
import {MatSliderChange} from "@angular/material/slider";
import {ModuleParticipationStatus} from "../../../../../services/generated/models/module-participation-status";

@Component({
  selector: 'app-administrate-module',
  templateUrl: './administrate-module.component.html',
  styleUrls: ['./administrate-module.component.scss']
})
export class AdministrateModuleComponent implements OnInit {

  public module: ModuleDetailItem | null = null;
  public savingInProgress: boolean = false;
  public isEditingName: boolean = false;

  public mId : string = "";

  public moduleName: string | null | undefined = "";
  public newModuleName: string = "";

  public moduleDescription: string | null | undefined = "";

  private isDeleting : boolean = false;
  private isArchiving: boolean = false;
  public showLoading: boolean = false;

  public nrParticipants: number|null = 0;
  public nrParticipantsText: string = "0";
  public isEditingPart: boolean = false;
  public participationStatus: ModuleParticipationStatus | undefined;
  moduleParticipationStatus = ModuleParticipationStatus;


  constructor(private readonly activatedRoute:ActivatedRoute,
              private readonly moduleService: ModuleService,
              public snackBar: MatSnackBar,
              private dialog: MatDialog,
              private router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      if(params["id"] != null){
        this.loadModule(params["id"])
      }
    })
  }

  loadModule(moduleId:string){
    if (moduleId == "") return

    this.moduleService.apiModuleGetModuleParticipationStatusGet$Json({
      moduleId: moduleId
    })
      .subscribe(data => this.participationStatus = data)

    this.moduleService.apiModuleGetByIdGet$Json({
      id: moduleId
    })
      .subscribe(m => {
        this.module = m;

        this.moduleName = this.module?.moduleName
        this.moduleDescription = this.module.moduleDescription;
        this.mId = moduleId;
        this.nrParticipants = this.module.maxParticipants ?? 200;

        if (this.nrParticipants == null || this.nrParticipants >= 200) {
          this.nrParticipantsText = "not limited"
        } else {
          this.nrParticipantsText = this.nrParticipants.toString();
        }
      })
  }

  nameEditButton() {
    this.isEditingName = !this.isEditingName;

    if (this.newModuleName == "") {
      // @ts-ignore
      this.newModuleName = this.module?.moduleName;
    }

    if (!this.isEditingName) {
      this.moduleName = this.newModuleName;
    }
  }

  async saveModuleChanges() {
    if (this.savingInProgress) return;

    this.showLoading = true;

    this.savingInProgress = true;
    //BACKEND API POST ROUTE to change user info
    await lastValueFrom(this.moduleService.apiModuleModuleIdPut({
      moduleId: this.mId,
      body: {
        moduleName: this.moduleName,
        moduleDescription: this.moduleDescription,
        maxParticipants: this.nrParticipants,
      }
    }));
    this.snackBar.open("Successfully saved changes!", "Dismiss", {duration:2000})
    await lastValueFrom(this.moduleService.apiModuleGetUsersForModuleGet$Json())
    this.savingInProgress = false;
    this.showLoading = false;
  }

  openDeleteDialog() {
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

  deleteModule() {
    if (this.isDeleting) {
      return;
    }
    this.showLoading = true;
    this.isDeleting = true;

    lastValueFrom(this.moduleService.apiModuleModuleIdDelete({
      moduleId: this.module!.moduleId!
    }))
      .catch(err =>{
        this.snackBar.open("Could not delete Module", "Dismiss", {duration:5000})
        this.showLoading = false;
        this.isDeleting = false;
        throw err
      }).then(() => {
      this.isDeleting = false;
      this.showLoading = false;
      this.snackBar.open("Successfully deleted Module!", "Dismiss", {duration:5000})
      this.router.navigateByUrl('/modules-admin');
    })
  }

  handleArchiving() {
    if (this.module?.isArchived) {
      this.unArchiveModule();
    } else {
      this.openArchiveDialog();
    }

    this.loadModule(this.mId);
  }

  private openArchiveDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;
    const dialogRef = this.dialog.open(ArchiveDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => {
        if (data == "archive") this.archiveModule();
      }
    );
  }

  private archiveModule() {
    if (this.isArchiving) {
      return;
    }

    this.showLoading = true;
    this.isArchiving = true;

    lastValueFrom(this.moduleService.apiModuleArchivePost({
      moduleId: this.module!.moduleId!
    }))
      .catch(err =>{
        this.snackBar.open("Could not archive Module", "Dismiss", {duration:5000});
        this.showLoading = false;
        this.isArchiving = false;
        throw err
      }).then(() => {
      this.snackBar.open("Successfully archived Module", "Dismiss", {duration:2000});
      this.showLoading = false;
      this.isArchiving = false;
      this.loadModule(this.mId);
    })
  }

  private unArchiveModule() {
    if (this.isArchiving) {
      return;
    }

    this.showLoading = true;
    this.isArchiving = true;

    lastValueFrom(this.moduleService.apiModuleUnarchivePost({
      moduleId: this.module!.moduleId!
    }))
      .catch(err =>{
        this.snackBar.open("Could not unarchive Module", "Dismiss", {duration:5000});
        this.showLoading = false;
        this.isArchiving = false;
        throw err
      }).then(() => {
      this.snackBar.open("Successfully unarchived Module", "Dismiss", {duration:2000});
      this.showLoading = false;
      this.isArchiving = false;
      this.loadModule(this.mId);
    })
  }

  changePartNr($event: MatSliderChange) {
    let value = $event.value;
    this.nrParticipants = Number(value);

    if (value == null || value > 200) {
      this.nrParticipantsText = "not limited"
      this.nrParticipants = null;
    } else {
      // @ts-ignore
      this.nrParticipantsText = this.nrParticipants.toString();
    }
  }

  partEditButton() {
    this.isEditingPart = !this.isEditingPart;
  }
}
