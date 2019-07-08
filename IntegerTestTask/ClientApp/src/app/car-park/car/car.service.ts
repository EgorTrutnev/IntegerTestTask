import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Car } from '../car-park.service';

@Injectable()
export class CarService {

  private url = "/api/carpark";

  constructor(private http: HttpClient) {
  }

  getCars() {
    return this.http.get(this.url + "/getcars");
  }

  createCar(car: Car) {
    return this.http.post(this.url + "/postcar", car);
  }

  updateCar(car: Car) {
    return this.http.put(this.url + '/putcar/' + car.carId, car);
  }

  deleteCar(id: number) {
    return this.http.delete(this.url + '/deletecar/' + id);
  }
}
