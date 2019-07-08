"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Car = /** @class */ (function () {
    function Car(carId, year, model, stateNumber, registrationDate) {
        this.carId = carId;
        this.year = year;
        this.model = model;
        this.stateNumber = stateNumber;
        this.registrationDate = registrationDate;
    }
    return Car;
}());
exports.Car = Car;
var TechInspection = /** @class */ (function () {
    function TechInspection(inspectionId, carId, cardNumber, dateTechInspection, comment) {
        this.inspectionId = inspectionId;
        this.carId = carId;
        this.cardNumber = cardNumber;
        this.dateTechInspection = dateTechInspection;
        this.comment = comment;
    }
    return TechInspection;
}());
exports.TechInspection = TechInspection;
var ReportCarPark = /** @class */ (function () {
    function ReportCarPark(totalCars, totalTechInspection, carsOver3Year, carsUnder3Year) {
        this.totalCars = totalCars;
        this.totalTechInspection = totalTechInspection;
        this.carsOver3Year = carsOver3Year;
        this.carsUnder3Year = carsUnder3Year;
    }
    return ReportCarPark;
}());
exports.ReportCarPark = ReportCarPark;
//# sourceMappingURL=car-park.service.js.map