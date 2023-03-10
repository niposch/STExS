import {NgModule} from '@angular/core';
import { BrowserModule} from '@angular/platform-browser';
import {RouterModule} from '@angular/router';
import {LayoutModule} from './layout/layout.module';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {ApiModule} from "../services/generated/api.module";
import {DashboardComponent} from './components/students/dashboard/dashboard.component';
import {LandingPageComponent} from './components/landing-page/landing-page.component';
import {ModulesAdminComponent} from './components/admin/modules-admin/modules-admin.component';
import {ModulesUserComponent} from './components/students/modules-user/modules-user.component';
import {ModuleComponent} from './components/module/module.component';

import {MatButtonModule} from "@angular/material/button";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatIconModule} from '@angular/material/icon'
import {MatDividerModule} from "@angular/material/divider";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {MatCardModule} from "@angular/material/card";
import {ProfileComponent} from "./components/profile/profile.component";
import {EditButtonComponent} from './components/profile/edit-button/edit-button.component';
import {UserInfoLabelComponent} from './components/profile/user-info-label/user-info-label.component';
import {MatInputModule} from "@angular/material/input";
import {FormsModule} from "@angular/forms";
import {MatChipsModule} from "@angular/material/chips";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {ModuleCreateComponent} from './components/admin/modules-admin/module-create/module-create.component';
import {ModuleListComponent} from './components/admin/modules-admin/module-list/module-list.component';
import {JoinModuleComponent} from './components/module/join-module/join-module.component';
import {MatExpansionModule} from "@angular/material/expansion";
import {MatListModule} from "@angular/material/list";
import {MatTableModule} from "@angular/material/table";
import {MatPaginatorModule} from "@angular/material/paginator";
import {ModuleDetailsComponent} from './components/module/module-details/module-details.component';
import {MatGridListModule} from "@angular/material/grid-list";
import {MatDialogModule} from "@angular/material/dialog";
import {DeleteDialogComponent} from "./components/module/delete-dialog/delete-dialog.component";
import {AdministrateModuleComponent} from './components/admin/modules-admin/administrate-module/administrate-module.component';
import {ChapterAdminListComponent} from './components/admin/chapter/chapter-admin-list/chapter-admin-list.component';
import {ChapterAdminListitemComponent} from './components/admin/chapter/chapter-admin-listitem/chapter-admin-listitem.component';
import {ArchiveDialogComponent} from './components/module/archive-dialog/archive-dialog.component';
import {MatSliderModule} from "@angular/material/slider";
import {ChapterAdminAdministrateComponent} from './components/admin/chapter/chapter-admin-administrate/chapter-admin-administrate.component';
import {TaskListComponent} from './components/admin/exercise-admin/task-list/task-list.component';
import {TaskListItemComponent} from './components/admin/exercise-admin/task-list-item/task-list-item.component';
import {DragDropModule} from "@angular/cdk/drag-drop";
import {CreateExerciseComponent} from './components/admin/exercise-admin/create-exercise/create-exercise.component';
import {MatRadioModule} from "@angular/material/radio";
import {MemberListAdminComponent} from './components/admin/modules-admin/member-list-admin/member-list-admin.component';
import {CreateEditCodeOutputComponent} from './components/admin/exercise-admin/create-exercise/create-edit-code-output/create-edit-code-output.component';
import {SolveCodeOutputComponent} from './components/students/solve-exercise/solve-code-output/solve-code-output.component';
import {SolveExerciseComponent} from './components/students/solve-exercise/solve-exercise.component';
import {MatTooltipModule} from "@angular/material/tooltip";
import { CreateEditParsonComponent } from './components/admin/exercise-admin/create-exercise/create-edit-parson/create-edit-parson.component';
import {QuillModule} from "ngx-quill";
import {CdkAccordionModule} from "@angular/cdk/accordion";
import { GradingExerciseDashboardComponent } from './components/admin/grading/grading-exercise-dashboard/grading-exercise-dashboard.component';
import { SubmissionStatePipe } from './pipes/submission/submission-state.pipe';
import { SubmissionGradingStatePipe } from './pipes/submission/submission-grading-state.pipe';
import { IndentedDropListComponent } from './components/helper/indented-drop-list/indented-drop-list.component';
import { ConfirmEmailComponent } from './components/authentication/confirm-email/confirm-email.component';
import { AdministrateUsersComponent } from './components/admin/administrate-users/administrate-users.component';
import {MatSortModule} from "@angular/material/sort";
import {MatMenuModule} from "@angular/material/menu";
import { ChangeRoleDialogComponent } from './components/admin/administrate-users/change-role-dialog/change-role-dialog.component';
import {MatSelectModule} from "@angular/material/select";
import { NoAccessComponent } from './components/no-access/no-access.component';
import { SolveGapTextComponent } from './components/students/solve-exercise/solve-gap-text/solve-gap-text.component';
import { CreateEditClozeComponent } from './components/admin/exercise-admin/create-edit-cloze/create-edit-cloze.component';
import { DataDashboardComponent } from './components/admin/data-dashboard/data-dashboard.component';
import { ViewClozeComponent } from './components/admin/exercise-admin/create-edit-cloze/view-cloze/view-cloze.component';
import { SolveParsonPuzzleComponent } from './components/students/solve-exercise/solve-parson-puzzle/solve-parson-puzzle.component';
import { RevisionHistoryComponent } from './components/admin/grading/revision-history/revision-history.component';
import { TimeTrackEventTypePipe } from './pipes/timeTrack/time-track-event-type.pipe';
import { RevisionItemComponent } from './components/admin/grading/revision-history/revision-item/revision-item.component';
import { GradingDialogComponent } from './components/admin/grading/grading-dialog/grading-dialog.component';
import { ExerciseTypePipe } from './pipes/exercise-type/exercise-type.pipe';
import { ClozeTextPreviewComponent } from './components/students/preview/cloze-text-preview/cloze-text-preview.component';
import { CodeOutputPreviewComponent } from './components/students/preview/code-output-preview/code-output-preview.component';
import { ParsonPreviewComponent } from './components/students/preview/parson-preview/parson-preview.component';
import { PreviewComponent } from './components/students/preview/preview.component';
import { PointsPanelComponent } from './components/students/solve-exercise/points-panel/points-panel.component';
import { GradingStatePipe } from './grading-state.pipe';

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
    MemberListAdminComponent,
    CreateEditCodeOutputComponent,
    SolveCodeOutputComponent,
    SolveExerciseComponent,
    CreateEditParsonComponent,
    SolveGapTextComponent,
    GradingExerciseDashboardComponent,
    SubmissionStatePipe,
    SubmissionGradingStatePipe,
    IndentedDropListComponent,
    ConfirmEmailComponent,
    AdministrateUsersComponent,
    ChangeRoleDialogComponent,
    NoAccessComponent,
    DataDashboardComponent,
    CreateEditClozeComponent,
    ViewClozeComponent,
    SolveParsonPuzzleComponent,
    RevisionHistoryComponent,
    TimeTrackEventTypePipe,
    RevisionItemComponent,
    GradingDialogComponent,
    ExerciseTypePipe,
    ClozeTextPreviewComponent,
    CodeOutputPreviewComponent,
    ParsonPreviewComponent,
    PreviewComponent,
    PointsPanelComponent,
    GradingStatePipe,
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
    DragDropModule,
    MatRadioModule,
    MatTooltipModule,
    QuillModule.forRoot({
      modules: {
        syntax: true,
        clipboard: true
      }
    }),
    CdkAccordionModule,
    MatSortModule,
    MatMenuModule,
    MatSelectModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
