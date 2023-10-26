import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { SettingsComponent } from './settings/settings.component';
import { TimeEntryComponent } from './time-entry/time-entry.component';
import { EmployeeHomeComponent } from './employee-home/employee-home.component';
import { LoginNavMenuComponent } from './nav-login-menu/nav-login-menu.component';
import { AdministrationToolsComponent } from './administration-tools/administration-tools.component';
import { TimeEntryApprovalComponent } from './time-entry-approval/time-entry-approval.component';
import { EmployeeAddComponent } from './employee-add/employee-add.component';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginNavMenuComponent,
    LoginComponent,
    SettingsComponent,
    TimeEntryComponent,
    TimeEntryApprovalComponent,
    EmployeeHomeComponent,
    AdministrationToolsComponent,
    EmployeeAddComponent,
    EmployeeEditComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'employee-home/:id', component: EmployeeHomeComponent },
      { path: 'employee-add/:id', component: EmployeeAddComponent },
      { path: 'employee-edit/:id/:employeeToEditId', component: EmployeeEditComponent },
      { path: 'settings/:id', component: SettingsComponent },
      { path: 'time-entry/:id', component: TimeEntryComponent },
      { path: 'admin/:id', component: AdministrationToolsComponent },
      { path: 'approve-time-sheets/:id', component: TimeEntryApprovalComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
