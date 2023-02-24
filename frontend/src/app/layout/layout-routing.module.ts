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
          {path: "dashboard", component: DashboardComponent},
          {path: "modules-user", component: ModulesUserComponent},
          {path: "modules-admin", component: ModulesAdminComponent},
          {path: "profile", component: ProfileComponent},
          {path: "module/details", component: ModuleDetailsComponent},
          {path: "module/administrate", component: AdministrateModuleComponent},
          {path: "module/administrate/chapter", component: ChapterAdminAdministrateComponent},
          {path: "codeoutput/create", component: CreateEditCodeOutputComponent},
          {path: "solve", component: SolveExerciseComponent},
          {path: "grading", component: GradingModuleDashboardComponent},
          {path: "grading/exercise", component: GradingExerciseDashboardComponent}
        ]
      },
      {
        path: "",
        component: HeaderOnlyLayoutComponent,
        children: [
          {path: "**", component: NotfoundComponent}
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
