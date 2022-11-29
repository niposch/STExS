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

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    LandingPageComponent,
    ModulesAdminComponent
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
    ApiModule.forRoot({rootUrl: '/api'})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
