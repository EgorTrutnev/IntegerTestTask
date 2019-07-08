import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { Routes, RouterModule } from '@angular/router';
import { AngularPaginatorModule } from 'angular-paginator';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BoxBallsSchemeComponent } from './box-balls-scheme/box-balls-scheme.component';
import { BoxBallsSchemeAlg1Component } from './box-balls-scheme/algorithm1/algorithm1.component';
import { BoxBallsSchemeAlg2Component } from './box-balls-scheme/algorithm2/algorithm2.component';
import { BoxBallsDataComponent } from './box-balls-data/box-balls-data.component';
import { SampleTableComponent } from './sample-table/sample-table.component';
import { CarParkComponent } from './car-park/car-park.component';
import { CarComponent } from './car-park/car/car.component';
import { TechInspectionComponent } from './car-park/techinspection/techinspection.component';
import { ReportComponent } from './car-park/report/report.component';
import { TaskProject } from './task/task.component';
import { GitHubLog } from './github-log/github-log.component';
import { NotFoundComponent } from './not-found/not-found.component';
 
const BoxBallsAlgorithmRoutes: Routes = [
  { path: '', component: BoxBallsSchemeAlg1Component },
  { path: 'alg2', component: BoxBallsSchemeAlg2Component }
];

const CarParkRoutes: Routes = [
  { path: '', component: CarComponent },
  { path: 'techinspection', component: TechInspectionComponent },
  { path: 'report', component: ReportComponent }
];

const appRoutes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'box-balls-scheme', component: BoxBallsSchemeComponent, children: BoxBallsAlgorithmRoutes },
  { path: 'box-balls-data', component: BoxBallsDataComponent },
  { path: 'sample-tables', component: SampleTableComponent },
  { path: 'car-park-tables', component: CarParkComponent, children: CarParkRoutes },
  { path: 'task', component: TaskProject },
  { path: 'github-log', component: GitHubLog },
  { path: '**', component: NotFoundComponent }
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
    SampleTableComponent,
    CarParkComponent,
    CarComponent,
    TechInspectionComponent,
    ReportComponent,
    GitHubLog,
    TaskProject,
    NotFoundComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    NgSelectModule,
    FormsModule,
    AngularPaginatorModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

export interface ServerSettings {
  maxBallsCount: number;
  versionProduct: string;
}
