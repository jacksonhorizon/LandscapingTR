import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { TimeEntryModel } from '../core/models/time/time-entry.model';
import { EmployeeService } from '../core/services/employee.service';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { LandscapingTRLookupsModel } from '../core/models/landscaping-tr-lookups.model';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { JobModel } from '../core/models/domain/job.model';
import { TimeEntryWeekModel } from '../core/models/time/week-entry-week.model';
import { JobTypes } from '../core/enums/job-types.enum';
import { TimeEntryService } from '../core/services/time-entry.service';
import { JobService } from '../core/services/job.service';
import { forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-time-entry',
  styleUrls: ['./time-entry.component.css'],
  templateUrl: './time-entry.component.html'
})
export class TimeEntryComponent {
  loaded: boolean = false;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;
  lookupsModel!: LandscapingTRLookupsModel;

  // General properties
  jobs: JobModel[] = []
  forms: TimeEntryWeekModel[] = [];
  weekStartDate: Date | null = null;
  weekEndDate: Date | null = null;

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private jobService: JobService,
    private timeEntryService: TimeEntryService,
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
      this.jobService.getJobsByEmployeeId(this.employeeId, this.weekStartDate, this.weekStartDate),
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];

        this.jobs = data[1];

        this.jobs.forEach(job => {
          var timeEntryWeek = this.generateWeeklyTimeEntryModel(this.employeeId, this.employeeModel.employeeTypeId, job.id, job.jobTypeId);

          if (!job.isCompleted) {
            this.forms.push(timeEntryWeek);
          }
        });

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

  private generateWeeklyTimeEntryModel(employeeId: number, employeeTypeId: number | undefined, jobId: number | undefined, jobTypeId: number | undefined) {
    const timeEntryWeek: TimeEntryWeekModel = {
      sunday: null,
      monday: null,
      tuesday: null,
      wednesday: null,
      thursday: null,
      friday: null,
      saturday: null,
      employeeId: employeeId,
      employeeTypeId: employeeTypeId,
      jobId: jobId,
      jobTypeId: jobTypeId,
    };

    return timeEntryWeek;
  }

  private getMockJobs() {
    const job1: JobModel = {
      id: 1,
      jobTypeId: JobTypes.ArtisticLandscaping
    };

    const job2: JobModel = {
      id: 3,
      jobTypeId: JobTypes.CommercialLandscaping
    };

    const job3: JobModel = {
      id: 25,
      jobTypeId: JobTypes.RoutineMaintenance
    };

    this.jobs.push(job1);
    this.jobs.push(job2);
    this.jobs.push(job3);
  }

  getAdminType() {
    return EmployeeTypes.Administrator as number;
  }

  getSupervisorType() {
    return EmployeeTypes.CrewSupervisor as number;
  }

  rerouteToApprovalPage(data: EmployeeModel): void {
    this.router.navigate(["approve-time-sheets/:" + data.id])
  }

  saveTimesheet() {
    var allTimeEntries: TimeEntryModel[] = [];
    this.forms.forEach(form => {
      var timeEntries = this.createTimeEntries(form);

      timeEntries.forEach(entry => allTimeEntries.push(entry));
    });

    var newTimeEntries = allTimeEntries.filter(x => x.id == null);

    var updateTimeEntries = allTimeEntries.filter(x => x.id != null);

    this.timeEntryService.saveNewTimeEntries(newTimeEntries).subscribe({
      next: data => {

        console.log(data);

        this.toastr.success('Time Entries were saved successfully!', 'Saved Time Entires: ');

      },
      error: err => {
        console.log(err);
      }
    });
  }


  createTimeEntries(timeEntryWeek: TimeEntryWeekModel): TimeEntryModel[] {
    var timeEntries: TimeEntryModel[] = [];

    if (timeEntryWeek.sunday != null && timeEntryWeek.sunday != 0) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date(),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.sunday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.monday != null && timeEntryWeek.monday != 0) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date(),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.monday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.tuesday != null && timeEntryWeek.tuesday != 0) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date(),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.tuesday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.wednesday != null && timeEntryWeek.wednesday != 0) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date(),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.wednesday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.thursday != null && timeEntryWeek.thursday != 0) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date(),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.thursday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.friday != null && timeEntryWeek.friday != 0) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date(),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.friday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.saturday != null && timeEntryWeek.saturday != 0) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date(),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.saturday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false
      }

      timeEntries.push(timeEntry);
    }

    return timeEntries;
  } 
}
