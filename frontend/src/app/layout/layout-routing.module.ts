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
      }
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
          {path: "profile", component: ProfileComponent}
        ]
      },
	    {
        path: "",
        component: MainLayoutComponent,
        children: [
          {path: "modules-admin", component: ModulesAdminComponent}
        ]
      },
    ]
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule {
}
