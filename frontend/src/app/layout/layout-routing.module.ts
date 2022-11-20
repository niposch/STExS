import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { HeaderOnlyLayoutComponent } from './header-only-layout/header-only-layout.component';

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
      { path: 'login', loadChildren: () => import('../components/authentication/login/login.module').then(x => x.LoginModule) },
      { path: 'register', loadChildren: () => import('../components/authentication/register/register.module').then(x => x.RegisterModule)}
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
