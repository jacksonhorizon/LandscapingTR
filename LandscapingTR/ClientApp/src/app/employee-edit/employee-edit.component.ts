import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { LandscapingTRLookupsModel } from '../core/models/landscaping-tr-lookups.model';
import { EmployeeService } from '../core/services/employee.service';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.css']
})
export class EmployeeEditComponent {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeToEditId!: number;
  pathEmployeeToEditId!: string;

  employeeModel!: EmployeeModel;
  employeeToEditModel!: EmployeeModel;
  lookupsModel!: LandscapingTRLookupsModel;

  // General properties
  form: any = {
    id: null,
    username: null,
    firstName: null,
    lastName: null,
    password: null,
    employeeTypeId: null,
  };

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
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
      },
      error: err => {
        console.log(err);
      }
    });


    // Gets the employee to edit Id
    const employeeToEditIdFromUrl = this.route.snapshot.paramMap.get('employeeToEditId')?.slice(1);
    if (employeeToEditIdFromUrl == null) {
      this.employeeToEditId = -1;
    } else {
      this.employeeToEditId = +employeeToEditIdFromUrl;
    }

    this.pathEmployeeToEditId = ":" + this.employeeToEditId.toString();

    // Gets employee to edit model
    this.employeeService.getEmployee(this.employeeToEditId).subscribe({
      next: data => {

        this.employeeToEditModel = data;
        this.form.id = this.employeeToEditModel.id;
        this.form.username = this.employeeToEditModel.username;
        this.form.firstName = this.employeeToEditModel.firstName;
        this.form.lastName = this.employeeToEditModel.lastName;
        this.form.password = this.employeeToEditModel.password;
        this.form.employeeTypeId = this.employeeToEditModel.employeeTypeId;

        this.loaded = true;
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
    const { id, firstName, lastName, username, password, employeeTypeId } = this.form;

    if (firstName == null || lastName == null || username == null || password == null || employeeTypeId == null) {
      return;
    }

    if (firstName === '' || lastName === '' || username === '' || password === '' || employeeTypeId === '') {
      return;
    }

    // code add employee method in service
    let newEmployeeModel = new EmployeeModel();

    newEmployeeModel.id = id;
    newEmployeeModel.username = username;
    newEmployeeModel.firstName = firstName;
    newEmployeeModel.lastName = lastName;
    newEmployeeModel.password = password;
    newEmployeeModel.employeeTypeId = employeeTypeId;

    this.employeeService.updateEmployee(newEmployeeModel).subscribe({
      next: data => {
        newEmployeeModel = data;
        this.form.id = newEmployeeModel.id;
        this.form.username = newEmployeeModel.username;
        this.form.firstName = newEmployeeModel.firstName;
        this.form.lastName = newEmployeeModel.lastName;
        this.form.password = newEmployeeModel.password;
        this.form.employeeTypeId = newEmployeeModel.employeeTypeId;
        this.toastr.success('Employee was saved successfully!', 'Saved Employee: ');
        this.router.navigate(["admin/:" + this.employeeModel.id])
      },
      error: err => {
        console.log(err);
      }
    });
  }

  onCancel(data: EmployeeModel): void {
    this.router.navigate(["admin/:" + data.id])
  }
}
