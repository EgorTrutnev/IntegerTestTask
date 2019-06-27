import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServerSettings } from '../app.module';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  public serversettings: ServerSettings = null;
  isExpanded = false;

  constructor(private http: HttpClient) {
    this.http.get<ServerSettings>("/api/server/serversettings").subscribe(result => {
      this.serversettings = result;
    });
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
