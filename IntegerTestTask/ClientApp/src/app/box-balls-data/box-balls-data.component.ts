import { Component, Injectable, ViewEncapsulation } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServerSettings } from '../app.module';
import { isObject } from 'util';

@Component({
  selector: 'app-box-balls-data',
  templateUrl: './box-balls-data.component.html',
  styleUrls: ['./box-balls-data.component.css'],
  encapsulation: ViewEncapsulation.None,
})

@Injectable()
export class BoxBallsDataComponent {
  public serversettings: ServerSettings = null;
  public boxballs: BoxBalls = null;
  private url: string;
  public globerrorload: boolean = false;
  public statusload: boolean = false;
  public errorload: string = null;

  constructor(private http: HttpClient) {
    this.http.get<ServerSettings>("/api/server/serversettings").subscribe(result => {
      this.serversettings = result;
      this.MainLogic();
    }, () => this.globerrorload = true);
  }

  private MainLogic(): void {
    $(document).ready(function () {
      let param: string[] = ["BallsCount", "Concentration", "BlackBalls", "WhiteBalls"];
      let defaultText: string = "Случайно";

      param.forEach(function (entry) {
        var input = <HTMLInputElement>document.getElementById(entry + "Input");
        var output = document.getElementById(entry + "Text");

        input.oninput = function () {
          let inputValue: number = parseInt(input.value);
          let maxInputValue: number = parseInt(input.name);
          if (entry == "BallsCount") {
            if (inputValue > maxInputValue) {
              input.value = output.innerHTML = maxInputValue.toString();
            }
            else if (inputValue == 0) {
              input.value = output.innerHTML = "";
            }
          }
          else if (entry == "Concentration") {
            if (inputValue > 100) {
              input.value = output.innerHTML = "100";
            }
          }
          else if (entry == "BlackBalls") {
            if (inputValue > maxInputValue) {
              input.value = output.innerHTML = maxInputValue.toString();
            }
          }
          else if (entry == "WhiteBalls") {
            if (inputValue > maxInputValue) {
              input.value = output.innerHTML = maxInputValue.toString();
            }
          }

          input.value == "" ? output.innerHTML = defaultText : output.innerHTML = input.value = String(parseInt(input.value.replace(/\D+/g, "")));

          if (input.value == "NaN") {
            output.innerHTML = defaultText;
            input.value = "";
          }
        }

      });
    })
  }

  private CreateBoxBalls(): void {
    this.statusload = true;
    this.boxballs = null;

    var ballscount: string = (<HTMLInputElement>document.getElementById("BallsCountInput")).value;
    let bconcentration: string = (<HTMLInputElement>document.getElementById("ConcentrationInput")).value;
    let blackballs: string = (<HTMLInputElement>document.getElementById("BlackBallsInput")).value;
    let whiteballs: string = (<HTMLInputElement>document.getElementById("WhiteBallsInput")).value;

    ballscount == "" ? ballscount = "-1" : ballscount;
    bconcentration == "" ? bconcentration = "-1" : bconcentration;
    blackballs == "" ? blackballs = "-1" : blackballs;
    whiteballs == "" ? whiteballs = "-1" : whiteballs;

    this.url = `api/boxballs/getboxballs?ballscount=${ballscount}&concentration=${bconcentration}&blackballs=${blackballs}&whiteballs=${whiteballs}`;

    this.http.get<BoxBalls>(this.url).subscribe(result => {
        this.boxballs = result;
        this.statusload = false;
    }, error => {
        this.errorload = error.message;
        this.statusload = false;
    });
  }
}

interface BoxBalls {
  ballsCount: number;
  concentration: number;
  blackBalls: number;
  whiteBalls: number;
  blackBallsNumbers: number[];
  whiteBallsNumbers: number[];
  errorCode: number;
  value: string;
}
