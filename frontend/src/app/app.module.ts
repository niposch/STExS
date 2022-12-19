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
    UserInfoLabelComponent
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
    MatChipsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
