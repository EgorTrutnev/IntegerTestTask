import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TechInspection, Car } from '../car-park.service';

@Injectable()
export class TechInspectionService {

  private url = "/api/carpark";

  constructor(private http: HttpClient) {
  }

  getTechInspections() {
    return this.http.get(this.url + "/gettechinspections");
  }

  createTechInspection(techinspection: TechInspection) {
    return this.http.post(this.url + "/posttechinspection", techinspection);
  }

  updateTechInspection(techinspection: TechInspection) {
    return this.http.put(this.url + '/puttechinspection/' + techinspection.inspectionId, techinspection);
  }

  deleteTechInspection(id: number) {
    return this.http.delete(this.url + '/deletetechinspection/' + id);
  }
}
