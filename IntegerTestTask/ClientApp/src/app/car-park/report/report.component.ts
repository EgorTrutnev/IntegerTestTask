import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReportCarPark } from '../car-park.service';
import { CarParkComponent } from '../car-park.component';

@Component({
  selector: 'report-component',
  templateUrl: './report.component.html'
})

export class ReportComponent {

  public report: ReportCarPark;
  public loadreportstatus: boolean = false;

  constructor(private http: HttpClient, private carParkComponent: CarParkComponent) {
    this.http.get<ReportCarPark>("/api/carpark/getreport").subscribe(result => {
      this.loadreportstatus = false;
      this.report = result;
    }, () => this.loadreportstatus = true);
  }
}
