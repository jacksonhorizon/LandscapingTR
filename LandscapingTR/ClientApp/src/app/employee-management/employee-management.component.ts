import { formatDate } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { LookupItemModel } from '../core/models/lookups/lookup-item.model';
import { EmployeeService } from '../core/services/employee.service';
import { LookupService } from '../core/services/lookup.service';
import { ConfirmationDialogService } from '../core/components/confirmation-dialog/confirmation-dialog.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee-management.component.html',
  styleUrls: ['./employee-management.component.css']
})
export class EmployeeManagementComponent implements OnInit {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;

  // General properties
  employees: EmployeeModel[] = [];
  employeeTypes!: LookupItemModel[];

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private lookupService: LookupService,
    private confirmationDialogService: ConfirmationDialogService,
    private router: Router,
    private toastr: ToastrService,) { }

  ngOnInit() {
    // Gets the employee Id
    const employeeIdFromUrl = this.route.snapshot.paramMap.get('id')?.slice(1);
    if (employeeIdFromUrl == null) {
      this.employeeId = -1;
    } else {
      this.employeeId = +employeeIdFromUrl;
    }

    this.pathEmployeeId = ":" + this.employeeId.toString();

    forkJoin([
      this.employeeService.getEmployee(this.employeeId),
      this.lookupService.getEmployeeTypes(),
      this.employeeService.getAllEmployees()
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];
        this.employeeTypes = data[1];
        this.employees = data[2];
        this.loaded = true; // Set loaded to true once all observables complete
      },
      error: err => {
        console.log(err);
      }
    });
  }

  // Is for the header
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  // General methods
  matchEmployeeType(employeeTypeId: number | undefined) {
    return this.employeeTypes.find(x => x.id === employeeTypeId)?.lookupValue || "Unknown";
  }

  getAdminType() {
    return EmployeeTypes.Administrator as number;
  }

  getSupervisorType() {
    return EmployeeTypes.CrewSupervisor as number;
  }

  getFormattedDate(dateToFormat: Date | undefined) {
    if (dateToFormat != undefined) {

      return formatDate(dateToFormat, "yyy-MM-dd", 'en-US');
    }

    return dateToFormat;
  }

  rerouteToAddEmployeePage(data: EmployeeModel): void {
    this.router.navigate(["employee-add/:" + data.id])
  }

  rerouteToEditEmployeePage(data: EmployeeModel, employeeClicked: EmployeeModel): void {
    this.router.navigate(["employee-edit/:" + data.id + "/:" + employeeClicked.id])
  }

  deleteEmployee(employeeClicked: EmployeeModel) {
    this.confirmationDialogService.confirm('Please confirm:', 'Are you sure you want to delete this employee?')
      .then((confirmed) => {
        console.log('User confirmed:', confirmed)
        // do the delete
      })
      .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }
}
