import { Component, OnInit } from '@angular/core';
import { CarService } from './car.service';
import { Car, TechInspection } from '../car-park.service';
import { TechInspectionService } from '../techinspection/techinspection.service';
import { CarParkComponent } from '../car-park.component';
import * as $ from 'jquery';
import 'bootstrap';

@Component({
  selector: 'car-component',
  templateUrl: './car.component.html',
  providers: [CarService, TechInspectionService]
})

export class CarComponent implements OnInit {

  car: Car = new Car();
  cars: Car[] = [];
  delCar: Car;
  techInspections: TechInspection[];
  carIndex: number = 0
  techInspectionsCount: number = 0;
  tableMode: boolean = true;
  modalEditCarMode: boolean = false;
  
  constructor(private carService: CarService,
    private techInspectionService: TechInspectionService,
    private carParkComponent: CarParkComponent) { }

  ngOnInit() {
    this.loadCars();
    this.loadTechInspection();
  }

  loadCars() {
    this.carService.getCars()
      .subscribe((data: Car[]) => this.cars = data);
  }

  loadTechInspection() {
    this.techInspectionService.getTechInspections()
      .subscribe((data: TechInspection[]) => this.techInspections = data);
  }

  saveperfom() {
    var point = true;
    var forms = document.getElementsByClassName('needs-validation');
    Array.prototype.filter.call(forms, form => {
      form.addEventListener('submit', event => {
        if (form.checkValidity() === true) {
          if (point) {
            point = !point;
            (<any>$('#modalCarEdit')).modal('hide');
            (<any>$('#modalCarAdd')).modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            if (this.car.carId == null)
              this.carService.createCar(this.car)
                .subscribe((data: Car) => {
                  this.cars.push(data);
                  this.loadCars();
                });
            else
              this.carService.updateCar(this.car)
                .subscribe(() => this.loadCars());

            this.cancel();
          }
        }
        else {
          form.classList.add('was-validated');
        }

        event.stopPropagation();
        event.preventDefault();
      }, false);
    });
  }

  save() {
    if (this.car.carId == null)
      this.carService.createCar(this.car)
        .subscribe((data: Car) => {
          this.cars.push(data);
          this.loadCars();
        });
    else
      this.carService.updateCar(this.car)
        .subscribe(() => this.loadCars());

    this.cancel();
  }

  editCar(c: Car, index: number) {
    this.modalEditCarMode = false;
    this.car = c;
    this.carIndex = index;
  }

  modalEditCar(c: Car) {
    Array.prototype.filter.call(document.getElementsByClassName('needs-validation'), form => form.classList.remove('was-validated'));
    this.cancel();
    this.modalEditCarMode = true;
    this.car = c;
  }

  cancel() {
    this.car = new Car();
    this.tableMode = true;
    this.carIndex = 0;
  }

  delete(c: Car) {
    this.carService.deleteCar(c.carId)
      .subscribe(() => this.loadCars());
  }

  modalDelete(c: Car) {
    this.techInspectionsCount = 0;
    this.delCar = c;
    this.techInspections.forEach(value => {
      if (value.carId == c.carId)
        this.techInspectionsCount = this.techInspectionsCount + 1;
    });
  }

  add() {
    this.cancel();
    this.tableMode = false;
  }

  modalAdd() {
    Array.prototype.filter.call(document.getElementsByClassName('needs-validation'), form => form.classList.remove('was-validated'));
    this.cancel();
  }
}
