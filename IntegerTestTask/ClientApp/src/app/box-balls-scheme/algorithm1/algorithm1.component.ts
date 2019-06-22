import { Component } from '@angular/core';
import { BoxBallsSchemeComponent } from '../../box-balls-scheme/box-balls-scheme.component';

@Component({
  selector: 'algorithm',
  templateUrl: './algorithm1.component.html',
})
export class BoxBallsSchemeAlg1Component {
  constructor() {
    new BoxBallsSchemeComponent();
  }
}
