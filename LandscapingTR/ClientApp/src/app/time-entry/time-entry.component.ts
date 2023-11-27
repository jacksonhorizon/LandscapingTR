import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TimeEntryModel } from '../core/models/time/time-entry.model';
import { EmployeeService } from '../core/services/employee.service';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { LandscapingTRLookupsModel } from '../core/models/landscaping-tr-lookups.model';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { JobModel } from '../core/models/domain/job.model';
import { TimeEntryWeekModel } from '../core/models/time/week-entry-week.model';
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
  timeEntries: TimeEntryModel[] = [];
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
      this.timeEntryService.getAllTimeEntriesByEmployeeId(this.employeeId)
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];

        this.jobs = data[1].filter(x => x.isCompleted != true);
        this.timeEntries = data[2];
        this.jobs.forEach(job => {
          var timeEntryWeek = this.generateWeeklyTimeEntryModel(this.employeeId, this.employeeModel.employeeTypeId, job.id, job.jobTypeId, this.timeEntries);

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

  private generateWeeklyTimeEntryModel(employeeId: number, employeeTypeId: number | undefined, jobId: number | undefined, jobTypeId: number | undefined, timeEntries: TimeEntryModel[]) {
    const sunday = timeEntries.find(entry => entry.dayNumber === 1 && entry.jobId === jobId && entry.employeeId === this.employeeId);
    const monday = timeEntries.find(entry => entry.dayNumber === 2 && entry.jobId === jobId && entry.employeeId === this.employeeId);
    const tuesday = timeEntries.find(entry => entry.dayNumber === 3 && entry.jobId === jobId && entry.employeeId === this.employeeId);
    const wednesday = timeEntries.find(entry => entry.dayNumber === 4 && entry.jobId === jobId && entry.employeeId === this.employeeId);
    const thursday = timeEntries.find(entry => entry.dayNumber === 5 && entry.jobId === jobId && entry.employeeId === this.employeeId);
    const friday = timeEntries.find(entry => entry.dayNumber === 6 && entry.jobId === jobId && entry.employeeId === this.employeeId);
    const saturday = timeEntries.find(entry => entry.dayNumber === 7 && entry.jobId === jobId && entry.employeeId === this.employeeId);

    const timeEntryWeek: TimeEntryWeekModel = {
      sunday: sunday?.totalLoggedHours,
      monday: monday?.totalLoggedHours,
      tuesday: tuesday?.totalLoggedHours,
      wednesday: wednesday?.totalLoggedHours,
      thursday: thursday?.totalLoggedHours,
      friday: friday?.totalLoggedHours,
      saturday: saturday?.totalLoggedHours,
      sundayId: sunday?.id,
      mondayId: monday?.id,
      tuesdayId: tuesday?.id,
      wednesdayId: wednesday?.id,
      thursdayId: thursday?.id,
      fridayId: friday?.id,
      saturdayId: saturday?.id,

      mondayApproved: monday?.isApproved,
      tuesdayApproved: tuesday?.isApproved,
      wednesdayApproved: wednesday?.isApproved,
      thursdayApproved: thursday?.isApproved,
      fridayApproved: friday?.isApproved,
      saturdayApproved: saturday?.isApproved,
      sundayApproved: sunday?.isApproved,

      employeeId: employeeId,
      employeeTypeId: employeeTypeId,
      jobId: jobId,
      jobTypeId: jobTypeId,
    };

    return timeEntryWeek;
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

  saveTimesheet(isSubmitted: boolean) {
    var allTimeEntries: TimeEntryModel[] = [];
    this.forms.forEach(form => {
      var timeEntries = this.createTimeEntries(form);

      timeEntries.forEach(entry => allTimeEntries.push(entry));
    });

    // If submitted
    if (isSubmitted) {
      allTimeEntries.forEach(x => x.isSubmitted = true);
    } else {
      allTimeEntries.forEach(x => x.isSubmitted = false);
      allTimeEntries.forEach(x => x.isApproved = false);
    }

    forkJoin([
      this.timeEntryService.saveNewTimeEntries(allTimeEntries)
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        if (isSubmitted) {

          this.toastr.success('Time Entries were submitted successfully!', 'Submitted Time Entries: ');
        } else {
          this.toastr.success('Time Entries were saved successfully!', 'Saved Time Entries: ');
        }

        // re-fill in form with saved time entries from data
        const savedTimeEntryModels = data[0];

        this.forms = [];
        this.jobs.forEach(job => {
          var timeEntryWeek = this.generateWeeklyTimeEntryModel(this.employeeId, this.employeeModel.employeeTypeId, job.id, job.jobTypeId, savedTimeEntryModels);

          if (!job.isCompleted) {
            this.forms.push(timeEntryWeek);
          }
        });
      },
      error: err => {
        console.log(err);
      }
    });
  }

  createTimeEntries(timeEntryWeek: TimeEntryWeekModel): TimeEntryModel[] {
    var timeEntries: TimeEntryModel[] = [];

    if (timeEntryWeek.sunday != null) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date('2023-11-26T03:24:00'),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.sunday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false,
        dayNumber: 1
      }

      if (timeEntryWeek.sundayId !== null) {
        timeEntry.id = timeEntryWeek.sundayId;
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.monday != null) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date('2023-11-27T03:24:00'),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.monday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false,
        dayNumber: 2
      }

      if (timeEntryWeek.mondayId !== null) {
        timeEntry.id = timeEntryWeek.mondayId;
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.tuesday != null) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date('2023-11-28T03:24:00'),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.tuesday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false,
        dayNumber: 3
      }

      if (timeEntryWeek.tuesdayId !== null) {
        timeEntry.id = timeEntryWeek.tuesdayId;
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.wednesday != null) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date('2023-11-29T03:24:00'),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.wednesday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false,
        dayNumber: 4
      }

      if (timeEntryWeek.wednesdayId !== null) {
        timeEntry.id = timeEntryWeek.wednesdayId;
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.thursday != null) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date('2023-11-30T03:24:00'),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.thursday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false,
        dayNumber: 5
      }

      if (timeEntryWeek.thursdayId !== null) {
        timeEntry.id = timeEntryWeek.thursdayId;
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.friday != null) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date('2023-12-01T03:24:00'),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.friday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false,
        dayNumber: 6
      }

      if (timeEntryWeek.fridayId !== null) {
        timeEntry.id = timeEntryWeek.fridayId;
      }

      timeEntries.push(timeEntry);
    }

    if (timeEntryWeek.saturday != null) {
      var timeEntry: TimeEntryModel = {
        employeeId: timeEntryWeek.employeeId,
        entryDate: new Date('2023-12-02T03:24:00'),
        employeeTypeId: timeEntryWeek.employeeTypeId,
        jobTypeId: timeEntryWeek.jobTypeId,
        jobId: timeEntryWeek.jobId,
        totalLoggedHours: timeEntryWeek.saturday,
        lastModifiedDate: new Date(),
        isSubmitted: false,
        isApproved: false,
        dayNumber: 7
      }

      if (timeEntryWeek.saturdayId !== null) {
        timeEntry.id = timeEntryWeek.saturdayId;
      }

      timeEntries.push(timeEntry);
    }

    return timeEntries;
  }


  timeIsSubmitted() {
    console.log(this.timeEntries.filter(x => x.employeeId == this.employeeId))
    if (this.timeEntries.filter(x => x.employeeId == this.employeeId).some(x => x.isSubmitted) == true) {
      return true;
    }

    return false;
  }
}
