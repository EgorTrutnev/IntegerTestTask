import { Component, OnInit } from '@angular/core';
import { TechInspectionService } from './techinspection.service';
import { TechInspection, Car } from '../car-park.service';
import { CarService } from '../car/car.service';
import { CarParkComponent } from '../car-park.component';
import * as $ from 'jquery';

@Component({
  selector: 'techinspection-component',
  templateUrl: './techinspection.component.html',
  providers: [CarService, TechInspectionService]
})

export class TechInspectionComponent implements OnInit {

  techInspection: TechInspection = new TechInspection();
  techInspections: TechInspection[] = [];
  delTechInspection: TechInspection;
  cars: Car[] = [];
  selectedCar: Car;

  constructor(private techInspectionService: TechInspectionService,
    private carService: CarService,
    private carParkComponent: CarParkComponent) { }

  ngOnInit() {
    this.loadTechInspection();
    this.loadCars();
  }

  loadTechInspection() {
    this.techInspectionService.getTechInspections()
      .subscribe((data: TechInspection[]) => {
        this.techInspections = data;
        this.carService.getCars()
          .subscribe((data: Car[]) => this.cars = data);
      });
  }

  loadCars() {
    this.carService.getCars()
      .subscribe((data: Car[]) => this.cars = data);
  }

  save() {
    var forms = document.getElementsByClassName('needs-validation');
    Array.prototype.filter.call(forms, form => {
      form.addEventListener('submit', event => {
        if (form.checkValidity() === true) {
          (<any>$('#modalTechInspectionEdit')).modal('hide');
          (<any>$('#modalTechInspectionAdd')).modal('hide');
          $('body').removeClass('modal-open');
          $('.modal-backdrop').remove();
          if (this.techInspection.inspectionId == null) {
            this.techInspectionService.createTechInspection(this.techInspection)
              .subscribe((data: TechInspection) => {
                this.techInspections.push(data);
                this.loadTechInspection();
              });
          } else {
            this.techInspectionService.updateTechInspection(this.techInspection)
              .subscribe(() => this.loadTechInspection());
          }
          this.cancel();
        }
        else {
          form.classList.add('was-validated');
        }

        event.stopPropagation();
        event.preventDefault();
      }, false);
    });
  }

  editTechInspection(ti: TechInspection) {
    Array.prototype.filter.call(document.getElementsByClassName('needs-validation'), form => form.classList.remove('was-validated'));
    this.techInspection = ti;
    this.loadTechInspection();
  }

  cancel() {
    this.techInspection = new TechInspection();
  }

  modalDelete(ti: TechInspection) {
    this.delTechInspection = ti;
    this.selectedCar = this.findCar(ti.carId)
  }

  delete(ti: TechInspection) {
    this.techInspectionService.deleteTechInspection(ti.inspectionId)
      .subscribe(() => this.loadTechInspection());
  }

  add() {
    Array.prototype.filter.call(document.getElementsByClassName('needs-validation'), form => form.classList.remove('was-validated'));
    this.cancel();
  }

  findCar(id: number): Car {
    return Array.prototype.filter.call(this.cars, x => x.carId == id)[0];
  }
}
