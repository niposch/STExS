import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {PreloadAllModules, RouterModule} from '@angular/router';
import {LayoutModule} from './layout/layout.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ApiModule } from "../services/generated/api.module";
import { DashboardComponent } from './components/students/dashboard/dashboard.component';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { ModulesAdminComponent } from './components/admin/modules-admin/modules-admin.component';
import { ModulesUserComponent } from './components/students/modules-user/modules-user.component';
import { ModuleComponent } from './components/module/module.component';

import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatIconModule } from '@angular/material/icon'
import { MatDividerModule } from "@angular/material/divider";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatCardModule } from "@angular/material/card";
import {ProfileComponent} from "./components/profile/profile.component";
import { EditButtonComponent } from './components/profile/edit-button/edit-button.component';
import { UserInfoLabelComponent } from './components/profile/user-info-label/user-info-label.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule} from "@angular/forms";
import {MatChipsModule} from "@angular/material/chips";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import { ModuleCreateComponent } from './components/admin/modules-admin/module-create/module-create.component';
import { ModuleListComponent } from './components/admin/modules-admin/module-list/module-list.component';
import { JoinModuleComponent } from './components/module/join-module/join-module.component';
import {MatExpansionModule} from "@angular/material/expansion";
import {MatListModule} from "@angular/material/list";
import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule} from "@angular/material/paginator";
import { ModuleDetailsComponent } from './components/module/module-details/module-details.component';
import {MatGridListModule} from "@angular/material/grid-list";
import {MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {DeleteDialogComponent} from "./components/module/delete-dialog/delete-dialog.component";
import { AdministrateModuleComponent } from './components/admin/modules-admin/administrate-module/administrate-module.component';
import { ChapterAdminListComponent } from './components/admin/chapter/chapter-admin-list/chapter-admin-list.component';
import { ChapterAdminListitemComponent } from './components/admin/chapter/chapter-admin-listitem/chapter-admin-listitem.component';
import { ArchiveDialogComponent } from './components/module/archive-dialog/archive-dialog.component';
import { MatSliderModule } from "@angular/material/slider";
import { ChapterAdminAdministrateComponent } from './components/admin/chapter/chapter-admin-administrate/chapter-admin-administrate.component';
import { TaskListComponent } from './components/admin/exercise-admin/task-list/task-list.component';
import { TaskListItemComponent } from './components/admin/exercise-admin/task-list-item/task-list-item.component';
import {DragDropModule} from "@angular/cdk/drag-drop";
import { CreateExerciseComponent } from './components/admin/exercise-admin/create-exercise/create-exercise.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LandingPageComponent,
    ModulesAdminComponent,
    ModulesUserComponent,
    ModuleComponent,
    ProfileComponent,
    EditButtonComponent,
    UserInfoLabelComponent,
    ModuleCreateComponent,
    ModuleListComponent,
    JoinModuleComponent,
    ModuleDetailsComponent,
    DeleteDialogComponent,
    AdministrateModuleComponent,
    ChapterAdminListComponent,
    ChapterAdminListitemComponent,
    ArchiveDialogComponent,
    ChapterAdminAdministrateComponent,
    TaskListComponent,
    TaskListItemComponent,
    CreateExerciseComponent,
  ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        RouterModule.forRoot([]),
        BrowserAnimationsModule,
        ApiModule.forRoot({rootUrl: ""}),
        MatButtonModule,
        MatCheckboxModule,
        MatIconModule,
        MatDividerModule,
        MatProgressSpinnerModule,
        MatCardModule,
        LayoutModule,
        MatInputModule,
        FormsModule,
        MatChipsModule,
        MatProgressBarModule,
        MatSnackBarModule,
        MatExpansionModule,
        MatListModule,
        MatTableModule,
        MatPaginatorModule,
        MatGridListModule,
        MatDialogModule,
        MatSliderModule,
        DragDropModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
