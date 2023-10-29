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
import { EmployeeManagementComponent } from './employee-management/employee-management.component';
import { TimeEntryApprovalComponent } from './time-entry-approval/time-entry-approval.component';
import { EmployeeAddComponent } from './employee-add/employee-add.component';
import { EmployeeEditComponent } from './employee-edit/employee-edit.component';
import { JobAddComponent } from './job-add/job-add.component';
import { JobManagementComponent } from './job-management/job-management.component';
import { JobEditComponent } from './job-edit/job-edit.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TrashCanComponent } from './core/components/trash-can/trash-can.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginNavMenuComponent,
    LoginComponent,
    SettingsComponent,
    TimeEntryComponent,
    TimeEntryApprovalComponent,
    EmployeeHomeComponent,
    EmployeeManagementComponent,
    EmployeeAddComponent,
    EmployeeEditComponent,
    JobAddComponent,
    JobEditComponent,
    JobManagementComponent,
    TrashCanComponent,
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
      { path: 'admin/:id', component: EmployeeManagementComponent },
      { path: 'approve-time-sheets/:id', component: TimeEntryApprovalComponent },
      { path: 'job-management/:id', component: JobManagementComponent },
      { path: 'job-add/:id', component: JobAddComponent },
      { path: 'job-edit/:id/:jobToEditId', component: JobEditComponent },
    ]),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
