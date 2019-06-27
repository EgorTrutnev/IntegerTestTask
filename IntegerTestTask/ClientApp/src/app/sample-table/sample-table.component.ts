import { Component, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-sample-table-component',
  templateUrl: './sample-table.component.html',
  styleUrls: ['./sample-table.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class SampleTableComponent {

  public students: Student;
  public subjectsOfStudy: SubjectsOfStudy;
  public learningOutcomes: LearningOutcomes;
  public tableParams: TableParams = null;

  public noselected: boolean = false;
  public loadtablesstatus: boolean = false;
  public statusload: boolean = false;
  public successloadresult: boolean = false;
  public loadingparam: number;
  public errorload: string;
  public nofound: string;

  public selectedSampleItem: number;
  public tableItemsPerPage: number = 5;
  public resultTableItemsPerPage: number = 5;
  itemsPageCount = [5, 10, 15, 20, 25];

  samples = [
    { id: 1, name: "Студенты, рождённые в СССР с оценкой не ниже 3 по информатике", description: "В выборке будут показаны студенты с оценками удовлетворительно и выше по информатике, дата рождения которых не позже 26 апреля 1991 года" },
    { id: 2, name: "Лучшие студенты (сдавшие экзамены на отлично)", description: "В выборке будут показаны студенты, средний балл по экзаменам у которых равен 5" },
    { id: 3, name: "Лучшие студенты вверху списка (по колличеству набарнных баллов)", description: "В выборке будут показаны студенты у которых наивысшее суммарное количество баллов" },
    { id: 4, name: "Количество студентов на курсах", description: "В выборке будут показаны курсы и общее количество студентов, обучающихся на каждом из них" },
    { id: 5, name: "Студенты, у кого сумма цифр даты рождения меньше 40", description: "В выборке будут показаны студенты, у которых сумма цифр даты рождения меньше 40 (например: 29.05.1999 = 2 + 9 + 0 + 5 + 1 + 9 + 9 + 9 = 44)" },
    { id: 6, name: "Студенты, обучающиеся на 6 курсе", description: "В выборке будут показаны студенты обучающиеся на 6 курсе" }
  ]

  constructor(private http: HttpClient) {
    this.http.get<Student>("/api/students/getstudents").subscribe(result => {
      this.students = result;
      console.log(this.students.dateOfBirthday);
    }, () => this.loadtablesstatus = true);

    this.http.get<SubjectsOfStudy>("/api/students/getsubjects").subscribe(result => {
      this.subjectsOfStudy = result;
    }, () => this.loadtablesstatus = true);

    this.http.get<LearningOutcomes>("/api/students/getlearns").subscribe(result => {
      this.learningOutcomes = result;
    }, () => this.loadtablesstatus = true);
  }

  private GetSampleDatas(): void {
    this.errorload = null;
    this.noselected = false;
    this.successloadresult = false;
    this.nofound = null;
    this.loadingparam = 0;

    if (this.selectedSampleItem <= 0 && this.selectedSampleItem > this.samples.length || this.selectedSampleItem == null)
      this.noselected = true;
    else {
      this.statusload = true;

      this.http.get<TableParams>("/api/students/getsample?param=" + this.selectedSampleItem).subscribe(result => {
        this.statusload = false;
        this.successloadresult = true;

        this.nofound = this.samples[this.selectedSampleItem - 1].name;
        this.tableParams = result;
        this.loadingparam = this.selectedSampleItem;

      }, error => {
        this.errorload = error.message;
        this.statusload = false;
      });
    }
  }
}

export class Student {
  studentId: number;
  name: string;
  dateOfBirthday: Date;
  studentYear: number;
}

export class SubjectsOfStudy {
  subjectId: number;
  title: string;
}

export class LearningOutcomes {
  id: number;
  studentId: number;
  subjectId: number;
  score: number;
}

class TableParams {
  studentId: number;
  name: string;
  dateOfBirthday: Date;
  studentYear: number;
  subject: number;
  score: number;
  averageScore: number;
  sumScore: number;
  studentsCount: number;
  sumBirth: number;
}
