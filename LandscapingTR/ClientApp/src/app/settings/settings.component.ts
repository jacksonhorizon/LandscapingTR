import { Component } from '@angular/core';

@Component({
  selector: 'app-settings-component',
  styleUrls: ['./settings.component.css'],
  templateUrl: './settings.component.html'
})
export class SettingsComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
