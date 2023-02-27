import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {MainLayoutComponent} from './main-layout/main-layout.component';
import {HeaderOnlyLayoutComponent} from './header-only-layout/header-only-layout.component';
import {DashboardComponent} from "../components/students/dashboard/dashboard.component";
import {LandingPageComponent} from "../components/landing-page/landing-page.component";
import {AuthGuard} from "../guards/auth.guard";
import {ModulesUserComponent} from "../components/students/modules-user/modules-user.component";
import {ModulesAdminComponent} from "../components/admin/modules-admin/modules-admin.component";
import {ProfileComponent} from "../components/profile/profile.component";
import {ModuleDetailsComponent} from "../components/module/module-details/module-details.component";
import {AdministrateModuleComponent} from "../components/admin/modules-admin/administrate-module/administrate-module.component";
import {ChapterAdminAdministrateComponent} from '../components/admin/chapter/chapter-admin-administrate/chapter-admin-administrate.component';
import {CreateEditCodeOutputComponent} from "../components/admin/exercise-admin/create-exercise/create-edit-code-output/create-edit-code-output.component";
import {NotfoundComponent} from "../components/notfound/notfound.component";
import {SolveExerciseComponent} from "../components/students/solve-exercise/solve-exercise.component";
import {
  GradingModuleDashboardComponent
} from "../components/admin/grading/grading-module-dashboard/grading-module-dashboard.component";
import {
  GradingExerciseDashboardComponent
} from "../components/admin/grading/grading-exercise-dashboard/grading-exercise-dashboard.component";
import {ConfirmEmailComponent} from "../components/authentication/confirm-email/confirm-email.component";
import {AdministrateUsersComponent} from "../components/admin/administrate-users/administrate-users.component";
import {RoleType} from "../../services/generated/models";
import {RoleGuard} from "../guards/role.guard";
import {NoAccessComponent} from "../components/no-access/no-access.component";
import {
  CreateEditClozeComponent
} from "../components/admin/exercise-admin/create-edit-cloze/create-edit-cloze.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  },
  // {
  //   path: '',
  //   component: MainLayoutComponent,
  //   children: [
  //     { path: 'dashboard', loadChildren: '../dashboard/dashboard.module#DashboardModule' },
  //     { path: 'users', loadChildren: '../users/users.module#UsersModule' },
  //     { path: 'account-settings', loadChildren: '../account-settings/account-settings.module#AccountSettingsModule' },
  //   ]
  // },
  {
    path: '',
    component: HeaderOnlyLayoutComponent,
    children: [
      {
        path: 'login',
        loadChildren: () => import('../components/authentication/login/login.module').then(x => x.LoginModule)
      },
      {
        path: 'register',
        loadChildren: () => import('../components/authentication/register/register.module').then(x => x.RegisterModule)
      },
      {path: 'confirm-email', component: ConfirmEmailComponent}
    ]
  },
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      {path: "", component: LandingPageComponent}
    ]
  },
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      {
        path: "",
        component: MainLayoutComponent,
        children: [
          {
            path: "",
            canActivate: [RoleGuard],
            data: {requiredRoles: [RoleType.Admin, RoleType.Teacher]},
            children: [
              {path: "modules-admin", component: ModulesAdminComponent},
              {path: "module/administrate", component: AdministrateModuleComponent},
              {path: "module/administrate/chapter", component: ChapterAdminAdministrateComponent},
              {path: "codeoutput/create", component: CreateEditCodeOutputComponent},
              {path: "cloze/create", component: CreateEditClozeComponent },
              {path: "grading", component: GradingModuleDashboardComponent},
              {path: "grading/exercise", component: GradingExerciseDashboardComponent},
            ]
          },
          {
            path: "",
            canActivate: [RoleGuard],
            data: {requiredRoles: [RoleType.Admin]},
            children: [
              {path: "users", component: AdministrateUsersComponent}
            ]
          },
          {path: "dashboard", component: DashboardComponent},
          {path: "modules-user", component: ModulesUserComponent},
          {path: "profile", component: ProfileComponent},
          {path: "module/details", component: ModuleDetailsComponent},
          {path: "solve", component: SolveExerciseComponent},
        ]
      },
      {
        path: "",
        component: HeaderOnlyLayoutComponent,
        children: [
          {path: "notfound", component: NotfoundComponent},
          {path: "forbidden", component: NoAccessComponent},
        ]
      }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule {
}
