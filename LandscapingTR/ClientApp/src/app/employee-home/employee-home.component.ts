import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { forkJoin } from 'rxjs';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { JobModel } from '../core/models/domain/job.model';
import { LandscapingTRLookupsModel } from '../core/models/landscaping-tr-lookups.model';
import { EmployeeService } from '../core/services/employee.service';
import { JobService } from '../core/services/job.service';

@Component({
  selector: 'app-employee-home',
  styleUrls: ['./employee-home.component.css'],
  templateUrl: './employee-home.component.html',
})
export class EmployeeHomeComponent {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;
  lookupsModel!: LandscapingTRLookupsModel;
  // General properties
  assignedJobs!: JobModel[];
  weekStartDate: Date | null = null;
  weekEndDate: Date | null = null;

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private jobService: JobService,  ) { }

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
      this.jobService.getJobsByEmployeeId(this.employeeId, this.weekStartDate, this.weekStartDate),
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];

        this.assignedJobs = data[1].filter(x => x.isCompleted != true);

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

  getHoursRemaining(job: JobModel) {
    if (job.estimatedTotalHours != undefined && job.totalLoggedHours != undefined) {
      return job.estimatedTotalHours - job.totalLoggedHours;
    } else {
      return 0;
    }
  }

  calculateJobPercentage(job: JobModel): number {
    if (job.estimatedTotalHours != undefined && job.totalLoggedHours != undefined) {
      if (job.totalLoggedHours === 0) {
        return 0;
      }
      var returnValue = ((job.totalLoggedHours / job.estimatedTotalHours) * 100);

      if (returnValue === 100) {
        return 100;
      } else if (returnValue < 100) {
        returnValue = +returnValue.toFixed(3);
        return returnValue
      } else {
        returnValue = +returnValue.toFixed(2);
        return returnValue
      }
    } else {
      return 0;
    }
  }
}
