import { Component } from '@angular/core';

@Component({
  selector: 'app-login-nav-menu',
  templateUrl: './nav-login-menu.component.html',
  styleUrls: ['./nav-login-menu.component.css']
})
export class LoginNavMenuComponent {
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
