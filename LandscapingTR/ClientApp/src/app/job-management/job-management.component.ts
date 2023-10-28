import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { JobModel } from '../core/models/domain/job.model';
import { formatDate } from '@angular/common';
import { LookupItemModel } from '../core/models/lookups/lookup-item.model';
import { EmployeeService } from '../core/services/employee.service';
import { JobService } from '../core/services/job.service';
import { LookupService } from '../core/services/lookup.service';

@Component({
  selector: 'app-job-management',
  templateUrl: './job-management.component.html',
  styleUrls: ['./job-management.component.css']
})
export class JobManagementComponent {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;

  // General properties
  jobs: JobModel[] = [];
  jobTypes!: LookupItemModel[];

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private lookupService: LookupService,
    private jobService: JobService,
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
      this.lookupService.getJobTypes(),
      this.jobService.getAllJobs()
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];
        this.jobTypes = data[1];
        this.jobs = data[2];
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
  matchJobType(jobTypeId: number | undefined) {
    return this.jobTypes.find(x => x.id === jobTypeId)?.lookupValue || "Unknown";
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

  getJobStatus(isCompleted: boolean | undefined) {
    if (isCompleted != undefined) {

      if (isCompleted) {

        return "Completed";
      }

      return "Incomplete";
    }
    

    return "Incomplete";
  }

  rerouteToAddJobPage(data: EmployeeModel): void {
    this.router.navigate(["job-add/:" + data.id])
  }

  rerouteToEditJobPage(data: EmployeeModel, jobClicked: JobModel): void {
    this.router.navigate(["job-edit/:" + data.id + "/:" + jobClicked.id])
  }
}
