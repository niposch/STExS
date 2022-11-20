import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {LandingPageComponent} from "./components/landing-page/landing-page.component";
import {HeaderOnlyLayoutComponent} from "./layout/header-only-layout/header-only-layout.component";

const routes: Routes = [
  {
    path: '',
    component: HeaderOnlyLayoutComponent,
    children: [
      {path: "", component: LandingPageComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
