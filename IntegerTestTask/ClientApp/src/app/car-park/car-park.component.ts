import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'car-park',
  templateUrl: './car-park.component.html',
  styleUrls: ['./car-park.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class CarParkComponent {
  private today: Date = new Date();
  private todayYear: number = this.today.getFullYear();
  private todayMonth: number = this.today.getMonth() + 1;
  private todayDay: number = this.today.getDate();
  public tableItemsPerPage: number = 5;
  public itemsPageCount = [5, 10, 15, 20, 25];

  constructor() {
  }

  getDateToday() {
    if (this.todayMonth < 10 && this.todayDay < 10)
      return this.todayYear + '-0' + this.todayMonth + '-0' + this.todayDay;
    else if (this.todayMonth < 10)
      return this.todayYear + '-0' + this.todayMonth + '-' + this.todayDay;
    else if (this.todayDay < 10)
      return this.todayYear + '-' + this.todayMonth + '-0' + this.todayDay;
    else
      return this.todayYear + '-' + this.todayMonth + '-' + this.todayDay;
  }

  getYearNow() {
    return this.todayYear;
  }

  DeclOfNum(n: number, titles: string[]) {
    return titles[(n % 10 == 1 && n % 100 != 11 ? 0 : n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20) ? 1 : 2)];
  }
}
