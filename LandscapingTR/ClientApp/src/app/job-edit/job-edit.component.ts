import { formatDate } from '@angular/common';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { forkJoin } from 'rxjs';
import { EmployeeTypes } from '../core/enums/employee-types.enum';
import { EmployeeModel } from '../core/models/company-resources/employee.model';
import { JobModel } from '../core/models/domain/job.model';
import { LookupItemModel } from '../core/models/lookups/lookup-item.model';
import { EmployeeService } from '../core/services/employee.service';
import { JobService } from '../core/services/job.service';
import { LookupService } from '../core/services/lookup.service';

@Component({
  selector: 'app-job-edit',
  templateUrl: './job-edit.component.html',
  styleUrls: ['./job-edit.component.css']
})
export class JobEditComponent {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;

  // General properties
  jobTypes!: LookupItemModel[];
  employees: EmployeeModel[] = [];

  jobToEditId!: number;
  pathJobToEditId!: string;
  jobToEditModel!: JobModel;

  form: any = {
    id: null,
    jobName: null,
    jobType: null,
    originalJobDate: null,
    jobDate: null,
    locationId: null,
    firstCrewMember: null,
    secondCrewMember: null,
    thirdCrewMember: null,
    fourthCrewMember: null,
    crewSupervisor: null,
    landscapeDesigner: null,
    equipmentAndSafetyOfficer: null,
    estimatedTotalHours: 0,
    isCompleted: false,
  };

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private jobService: JobService,
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

    // Gets the job to edit Id
    const jobToEditIdFromUrl = this.route.snapshot.paramMap.get('jobToEditId')?.slice(1);
    if (jobToEditIdFromUrl == null) {
      this.jobToEditId = -1;
    } else {
      this.jobToEditId = +jobToEditIdFromUrl;
    }

    this.pathJobToEditId = ":" + this.jobToEditId.toString();

    // Gets employee model
    forkJoin([
      this.employeeService.getEmployee(this.employeeId),
      this.employeeService.getAllEmployees(),
      this.jobService.getJobById(this.jobToEditId),
      this.lookupService.getJobTypes(),
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];
        this.employees = data[1];
        this.jobToEditModel = data[2];
        this.jobTypes = data[3];

        this.form.id = this.jobToEditModel.id;
        this.form.jobName = this.jobToEditModel.jobName;

        var jobType = this.jobTypes.find(x => x.id === this.jobToEditModel.jobTypeId)?.lookupValue || "";
        this.form.jobType = jobType;

        this.form.jobDate = this.jobToEditModel.jobDate as Date;
        this.form.originalJobDate = this.form.jobDate;

        var firstCrewMember = this.employees.find(x => x.id === this.jobToEditModel.firstCrewMemberId);
        this.form.firstCrewMember = firstCrewMember?.lastName + ", " + firstCrewMember?.firstName;

        var secondCrewMember = this.employees.find(x => x.id === this.jobToEditModel.secondCrewMemberId);
        this.form.secondCrewMember = secondCrewMember?.lastName + ", " + secondCrewMember?.firstName;

        var thirdCrewMember = this.employees.find(x => x.id === this.jobToEditModel.thirdCrewMemberId);
        this.form.thirdCrewMember = thirdCrewMember?.lastName + ", " + thirdCrewMember?.firstName;

        var fourthCrewMember = this.employees.find(x => x.id === this.jobToEditModel.fourthCrewMemberId);
        this.form.fourthCrewMember = fourthCrewMember?.lastName + ", " + fourthCrewMember?.firstName;

        var crewSupervisor = this.employees.find(x => x.id === this.jobToEditModel.crewSupervisorId);
        this.form.crewSupervisor = crewSupervisor?.lastName + ", " + crewSupervisor?.firstName;

        this.form.estimatedTotalHours = this.jobToEditModel.estimatedTotalHours;

        this.form.isCompleted = this.jobToEditModel.isCompleted;

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
    const { id, jobName, jobType, jobDate, firstCrewMember, secondCrewMember, thirdCrewMember, fourthCrewMember, crewSupervisor, estimatedTotalHours , isCompleted } = this.form;

    if (jobName === null || jobDate === null || jobType === null || estimatedTotalHours === null) {
      return;
    }

    if (jobName === '' || jobDate == '' || jobType === '' || estimatedTotalHours === '') {
      return
    }

    // code add employee method in service
    let newJobModel = new JobModel();

    newJobModel.id = id;
    newJobModel.jobName = jobName;

    var jobTypeId = this.jobTypes.find(x => x.lookupValue === jobType)?.id || 1;
    newJobModel.jobTypeId = jobTypeId;

    newJobModel.jobDate = jobDate;

    newJobModel.isCompleted = isCompleted;

    if (firstCrewMember !== null) {
      var firstCrewMemberId = this.employees.find(x => firstCrewMember.includes(x.firstName) && firstCrewMember.includes(x.lastName))?.id || undefined;
      newJobModel.firstCrewMemberId = firstCrewMemberId;
    }

    if (secondCrewMember !== null) {
      var secondCrewMemberId = this.employees.find(x => secondCrewMember.includes(x.firstName) && secondCrewMember.includes(x.lastName))?.id || undefined;
      newJobModel.secondCrewMemberId = secondCrewMemberId;
    }

    if (thirdCrewMember !== null) {
      var thirdCrewMemberId = this.employees.find(x => thirdCrewMember.includes(x.firstName) && thirdCrewMember.includes(x.lastName))?.id || undefined;
      newJobModel.thirdCrewMemberId = thirdCrewMemberId;
    }

    if (fourthCrewMember !== null) {
      var fourthCrewMemberId = this.employees.find(x => fourthCrewMember.includes(x.firstName) && fourthCrewMember.includes(x.lastName))?.id || undefined;
      newJobModel.fourthCrewMemberId = fourthCrewMemberId;
    }

    if (crewSupervisor !== null) {
      var crewSupervisorId = this.employees.find(x => crewSupervisor.includes(x.firstName) && crewSupervisor.includes(x.lastName))?.id || undefined;
      newJobModel.crewSupervisorId = crewSupervisorId;
    }

    newJobModel.estimatedTotalHours = estimatedTotalHours;

    this.jobService.updateJob(newJobModel).subscribe({
      next: data => {
        newJobModel = data;

        this.toastr.success('Job was saved successfully!', 'Saved Job: ');
        this.router.navigate(["job-management/:" + this.employeeModel.id])
      },
      error: err => {
        this.toastr.error("There was a probelm saving.", 'Error: ');
        console.log(err);
      }
    });
  }

  onCancel(data: EmployeeModel): void {
    this.router.navigate(["job-management/:" + data.id])
  }

  getFieldCrewWorkers() {
    return this.employees.filter(x => x.employeeTypeId == EmployeeTypes.FieldCrewWorker);
  }

  getSupervisors() {
    return this.employees.filter(x => x.employeeTypeId == EmployeeTypes.CrewSupervisor);
  }

  getFormattedDate(dateToFormat: Date | undefined) {
    if (dateToFormat != undefined) {

      return formatDate(dateToFormat, "MM-dd-yyyy", 'en-US');
    }

    return dateToFormat;
  }
}
