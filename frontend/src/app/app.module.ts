import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {PreloadAllModules, RouterModule} from '@angular/router';
import {LayoutModule} from './layout/layout.module';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule} from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './components/authentication/login/login.component';
import { RegisterComponent } from './components/authentication/register/register.component';

import {ApiModule} from "../services/generated/api.module";
import { DashboardComponent } from './components/students/dashboard/dashboard.component';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { ModulesAdminComponent } from './components/admin/modules-admin/modules-admin.component';
import { ModulesUserComponent } from './components/students/modules-user/modules-user.component';
import { ModuleComponent } from './components/module/module.component';

import {MatButtonModule} from "@angular/material/button";
import {MatCheckboxModule} from "@angular/material/checkbox";
import { MatIconModule } from '@angular/material/icon'
import {MatDividerModule} from "@angular/material/divider";



@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LandingPageComponent,
    ModulesAdminComponent,
    ModulesUserComponent,
    ModuleComponent,
    //LoginComponent,
    //RegisterComponent
  ],
    imports: [
        BrowserModule,
        HttpClientModule,
        AppRoutingModule,
        RouterModule.forRoot([]),
        // RouterModule.forRoot([], { preloadingStrategy: PreloadAllModules }),
        LayoutModule,
        BrowserAnimationsModule,
        ApiModule.forRoot({rootUrl:""}),
        MatButtonModule,
        MatCheckboxModule,
        MatIconModule,
        MatDividerModule,
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
