import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { LookupItemModel } from '../core/models/lookups/lookup-item.model';
import { EmployeeService } from '../core/services/employee.service';
import { LookupService } from '../core/services/lookup.service';

@Component({
  selector: 'app-employee-add',
  templateUrl: './employee-add.component.html',
  styleUrls: ['./employee-add.component.css']
})
export class EmployeeAddComponent {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;

  // General properties
  employeeTypes!: LookupItemModel[];
  form: any = {
    username: null,
    firstName: null,
    lastName: null,
    password: null,
    employeeType: "Field Crew Worker",
    payRate: 0
  };

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private lookupService: LookupService,
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

    // Gets employee model
    this.employeeService.getEmployee(this.employeeId).subscribe({
      next: data => {

        this.employeeModel = data;
        this.loaded = true;
      },
      error: err => {
        console.log(err);
      }
    });

    forkJoin([
      this.employeeService.getEmployee(this.employeeId),
      this.lookupService.getEmployeeTypes()
    ]).subscribe({
      next: data => {
        this.employeeModel = data[0];
        this.employeeTypes = data[1];

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

  getAdminType() {
    return EmployeeTypes.Administrator as number;
  }

  getSupervisorType() {
    return EmployeeTypes.CrewSupervisor as number;
  }

  onSubmit(): void {
    const { firstName, lastName, username, password, employeeType, payRate} = this.form;

    if (firstName == null || lastName == null || username == null || password == null || employeeType == null) {
      return; 
    }

    if (firstName === '' || lastName === '' || username === '' || password === '' || employeeType === '') {
      return;
    }

    // code add employee method in service
    let newEmployeeModel = new EmployeeModel();

    newEmployeeModel.username = username;
    newEmployeeModel.firstName = firstName;
    newEmployeeModel.lastName = lastName;
    newEmployeeModel.password = password;
    newEmployeeModel.payRate = payRate;
    newEmployeeModel.active = true;

    var employeeTypeId = this.employeeTypes.find(x => x.lookupValue === employeeType)?.id || 0;
    newEmployeeModel.employeeTypeId = employeeTypeId;

    this.employeeService.saveNewEmployee(newEmployeeModel).subscribe({
      next: data => {
        newEmployeeModel = data;

        this.toastr.success('Employee was saved successfully!', 'Saved Employee: ');
        this.router.navigate(["admin/:" + this.employeeModel.id])
      },
      error: err => {
        this.toastr.error("There was a probelm saving.", 'Error: ' + err);
        console.log(err);
      }
    });
  }

  onCancel(data: EmployeeModel): void {
    this.router.navigate(["admin/:" + data.id])
  }
}
