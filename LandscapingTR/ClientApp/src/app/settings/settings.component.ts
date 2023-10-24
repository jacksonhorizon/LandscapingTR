import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { LandscapingTRLookupsModel } from '../core/models/landscaping-tr-lookups.model';
import { EmployeeService } from '../core/services/employee.service';

@Component({
  selector: 'app-settings-component',
  styleUrls: ['./settings.component.css'],
  templateUrl: './settings.component.html'
})
export class SettingsComponent {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;
  lookupsModel!: LandscapingTRLookupsModel;

  // General properties
  form: any = {
    username: null,
    firstName: null,
    lastName: null
  };

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private router: Router) { }

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
        this.form.username = this.employeeModel.username;
        this.form.firstName = this.employeeModel.firstName;
        this.form.lastName = this.employeeModel.lastName;
        console.log(this.employeeModel)
      },
      error: err => {
        console.log(err);
      }
    });

    this.loaded = true;
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
    const { firstName, lastName, username } = this.form;

    console.log(this.form);
  }

  onCancel(data: EmployeeModel): void {
    this.router.navigate(["employee-home/:" + data.id])
  }
}
