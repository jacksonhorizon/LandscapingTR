import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { SettingsComponent } from './settings/settings.component';
import { TimeEntryComponent } from './time-entry/time-entry.component';
import { EmployeeHomeComponent } from './employee-home/employee-home.component';
import { LoginNavMenuComponent } from './nav-login-menu/nav-login-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,

    LoginNavMenuComponent,
    LoginComponent,
    SettingsComponent,
    TimeEntryComponent,
    EmployeeHomeComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'employee-home/:id', component: EmployeeHomeComponent },
      { path: 'settings/:id', component: SettingsComponent },
      { path: 'time-entry/:id', component: TimeEntryComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
