import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BoxBallsSchemeComponent } from './box-balls-scheme/box-balls-scheme.component';
import { BoxBallsSchemeAlg1Component } from './box-balls-scheme/algorithm1/algorithm1.component';
import { BoxBallsSchemeAlg2Component } from './box-balls-scheme/algorithm2/algorithm2.component';
import { BoxBallsDataComponent } from './box-balls-data/box-balls-data.component';
import { NotFoundComponent } from './not-found/not-found.component';
 
const algorithmRoutes: Routes = [
  { path: '', component: BoxBallsSchemeAlg1Component },
  { path: 'alg2', component: BoxBallsSchemeAlg2Component },
];

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'box-balls-scheme', component: BoxBallsSchemeComponent, children: algorithmRoutes },
  { path: 'box-balls-data', component: BoxBallsDataComponent },
  { path: '**', component: NotFoundComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    BoxBallsSchemeComponent,
    BoxBallsSchemeAlg1Component,
    BoxBallsSchemeAlg2Component,
    BoxBallsDataComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

export interface ServerSettings {
  maxballscount: number;
}
