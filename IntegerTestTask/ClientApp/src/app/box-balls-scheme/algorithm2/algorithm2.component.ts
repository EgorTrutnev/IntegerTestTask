import { Component } from '@angular/core';
import { BoxBallsSchemeComponent } from '../../box-balls-scheme/box-balls-scheme.component';

@Component({
  selector: 'algorithm',
  templateUrl: './algorithm2.component.html'
})
export class BoxBallsSchemeAlg2Component {
  constructor() {
    new BoxBallsSchemeComponent();
  }
}
