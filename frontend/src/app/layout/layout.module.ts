import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {MainLayoutComponent} from './main-layout/main-layout.component';
import {HeaderOnlyLayoutComponent} from './header-only-layout/header-only-layout.component';
import {HeaderComponent} from './header/header.component';
import {SidebarComponent} from './sidebar/sidebar.component';
import {LayoutRoutingModule} from './layout-routing.module';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatAutocompleteModule} from "@angular/material/autocomplete";
import {MatInputModule} from "@angular/material/input";
import {MatListModule} from "@angular/material/list";
import {MatSidenavModule} from "@angular/material/sidenav";
import {FormsModule} from "@angular/forms";
import { NotfoundComponent } from '../components/notfound/notfound.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { SidebarEntryComponent } from './sidebar/sidebar-entry/sidebar-entry.component';
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {CdkAccordionModule} from "@angular/cdk/accordion";
import {MatMenuModule} from "@angular/material/menu";
import {MatProgressBarModule} from "@angular/material/progress-bar";
import {QuillModule} from "ngx-quill";


@NgModule({
    declarations: [
        MainLayoutComponent,
        HeaderOnlyLayoutComponent,
        HeaderComponent,
        SidebarComponent,
        NotfoundComponent,
        SidebarEntryComponent,
    ],
    exports: [
        NotfoundComponent
    ],
  imports: [
    CommonModule,
    LayoutRoutingModule,
    MatButtonModule,
    MatIconModule,
    MatAutocompleteModule,
    MatInputModule,
    MatListModule,
    MatSidenavModule,
    FormsModule,
    MatExpansionModule,
    MatProgressSpinnerModule,
    CdkAccordionModule,
    MatMenuModule,
    MatProgressBarModule,
    QuillModule
  ]
})
export class LayoutModule {
}
