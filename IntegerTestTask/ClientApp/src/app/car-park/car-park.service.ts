export class Car {
  constructor(
    public carId?: number,
    public year?: number,
    public model?: string,
    public stateNumber?: string,
    public registrationDate?: Date) { }
}

export class TechInspection {
  constructor(
    public inspectionId?: number,
    public carId?: number,
    public cardNumber?: string,
    public dateTechInspection?: Date,
    public comment?: string) { }
}

export class ReportCarPark {
  constructor(
    public totalCars?: number,
    public totalTechInspection?: number,
    public carsOver3Year?: number,
    public carsUnder3Year?: number) { }
}
