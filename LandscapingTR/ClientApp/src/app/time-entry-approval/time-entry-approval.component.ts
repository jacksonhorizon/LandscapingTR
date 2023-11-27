import { formatDate, Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { JobModel } from '../core/models/domain/job.model';
import { LandscapingTRLookupsModel } from '../core/models/landscaping-tr-lookups.model';
import { LookupItemModel } from '../core/models/lookups/lookup-item.model';
import { TimeEntryModel } from '../core/models/time/time-entry.model';
import { EmployeeService } from '../core/services/employee.service';
import { JobService } from '../core/services/job.service';
import { LookupService } from '../core/services/lookup.service';
import { TimeEntryService } from '../core/services/time-entry.service';

@Component({
  selector: 'app-time-entry-approval',
  templateUrl: './time-entry-approval.component.html',
  styleUrls: ['./time-entry-approval.component.css']
})
export class TimeEntryApprovalComponent implements OnInit {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;
  lookupsModel!: LandscapingTRLookupsModel;

  // General properties
  timeEntries: TimeEntryModel[] = [];
  employees: EmployeeModel[] = [];
  jobs: JobModel[] = [];
  jobTypes!: LookupItemModel[];

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private timeEntryService: TimeEntryService,
    private lookupService: LookupService,
    private jobService: JobService,
    private toastr: ToastrService,
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

    forkJoin([
      this.employeeService.getEmployee(this.employeeId),
      this.timeEntryService.getAllSubmittedTimeEntries(),
      this.lookupService.getJobTypes(),
      this.employeeService.getAllEmployees(),
      this.jobService.getAllJobs()
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];

        this.timeEntries = data[1].filter(x => x.isSubmitted == true && x.isApproved == false);

        this.jobTypes = data[2];
        this.employees = data[3];
        this.jobs = data[4];


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

  approveTimeEntry(timeEntry: TimeEntryModel) {
    timeEntry.isApproved = true;

    this.timeEntryService.saveNewTimeEntry(timeEntry).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.toastr.success('Time Entry was approved successfully!', 'Approved Time Entry: ');

        // re-fill in form with saved time entries from data
        const savedTimeEntryModel = data;
        console.log(savedTimeEntryModel);
      },
      error: err => {
        console.log(err);
      }
    });
  }

  getFormattedDate(dateToFormat: Date | undefined) {
    if (dateToFormat != undefined) {

      return formatDate(dateToFormat, "MM-dd-yyyy", 'en-US');
    }

    return dateToFormat;
  }

  getFormattedDateTime(dateToFormat: Date | undefined) {
    if (dateToFormat != undefined) {

      return formatDate(dateToFormat, "MM-dd-yyyy HH:mm", 'en-US');
    }

    return dateToFormat;
  }

  getEmployeeName(employeeId: number | undefined | null) {
    if (employeeId !== undefined) {

      var employee = this.employees.find(x => x.id === employeeId);

      if (employee === null || employee === undefined) {
        return "";
      }
      else {
        return employee?.lastName + ", " + employee?.firstName;
      }
    }

    return employeeId;
  }

  getJobName(jobId: number | undefined | null) {
    if (jobId !== undefined) {

      var job = this.jobs.find(x => x.id === jobId);

      if (job === null || job === undefined) {
        return "";
      }
      else {
        return job.jobName;
      }
    }

    return jobId;
  }

  matchJobType(jobTypeId: number | undefined | null) {
    return this.jobTypes.find(x => x.id === jobTypeId)?.lookupValue || "Unknown";
  }

  deleteTimeEntry(timeEntry: TimeEntryModel) {
    timeEntry.totalLoggedHours = 0;
    this.timeEntryService.saveNewTimeEntry(timeEntry).subscribe({
      next: data => {

        this.timeEntryService.deleteTimeEntry(timeEntry).subscribe({
          next: data => {
            // data is an array containing the results of the observables in the same order
            this.toastr.error('Time Entry was rejected!', 'Rejected Time Entry: ');

            // re-fill in form with saved time entries from data
            const savedTimeEntryModel = data;
            console.log(savedTimeEntryModel);
          },
          error: err => {
            console.log(err);
          }
        });
      },
      error: err => {
        console.log(err);
      }
    });
  }


}
