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
  selector: 'app-job-add',
  templateUrl: './job-add.component.html',
  styleUrls: ['./job-add.component.css']
})
export class JobAddComponent {
  loaded!: boolean;
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  employeeModel!: EmployeeModel;

  // General properties
  jobTypes!: LookupItemModel[];
  employees: EmployeeModel[] = [];
  form: any = {
    jobName: null,
    jobType: "Routine Maintenance",
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

    forkJoin([
      this.employeeService.getEmployee(this.employeeId),
      this.lookupService.getJobTypes(),
      this.employeeService.getAllEmployees()
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];
        this.jobTypes = data[1];
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
  getAdminType() {
    return EmployeeTypes.Administrator as number;
  }

  getSupervisorType() {
    return EmployeeTypes.CrewSupervisor as number;
  }

  onSubmit(): void {
    const { jobName, jobType, jobDate, firstCrewMember, secondCrewMember, thirdCrewMember, fourthCrewMember, crewSupervisor, estimatedTotalHours } = this.form;

    if (jobName === null || jobDate === null || jobType === null || estimatedTotalHours === null) {
      return;
    }

    if (jobName === '' || jobDate == '' || jobType === '' || estimatedTotalHours === '') {
      return
    }

    // code add employee method in service
    let newJobModel = new JobModel();

    newJobModel.jobName = jobName;

    var jobTypeId = this.jobTypes.find(x => x.lookupValue === jobType)?.id || 1;
    newJobModel.jobTypeId = jobTypeId;

    newJobModel.jobDate = jobDate;


    if (firstCrewMember !== null) {
      var firstCrewMemberId = this.employees.find(x => firstCrewMember.includes(x.firstName) && firstCrewMember.includes(x.lastName))?.id || 1;
      newJobModel.firstCrewMemberId = firstCrewMemberId;
    }

    if (secondCrewMember !== null) {
      var secondCrewMemberId = this.employees.find(x => secondCrewMember.includes(x.firstName) && secondCrewMember.includes(x.lastName))?.id || 1;
      newJobModel.secondCrewMemberId = secondCrewMemberId;
    }

    if (thirdCrewMember !== null) {
      var thirdCrewMemberId = this.employees.find(x => thirdCrewMember.includes(x.firstName) && thirdCrewMember.includes(x.lastName))?.id || 1;
      newJobModel.thirdCrewMemberId = thirdCrewMemberId;
    }

    if (fourthCrewMember !== null) {
      var fourthCrewMemberId = this.employees.find(x => fourthCrewMember.includes(x.firstName) && fourthCrewMember.includes(x.lastName))?.id || 1;
      newJobModel.fourthCrewMemberId = fourthCrewMemberId;
    }

    if (crewSupervisor !== null) {
      var crewSupervisorId = this.employees.find(x => crewSupervisor.includes(x.firstName) && crewSupervisor.includes(x.lastName))?.id || 1;
      newJobModel.crewSupervisorId = crewSupervisorId;
    }
    
    newJobModel.estimatedTotalHours = estimatedTotalHours;

    this.jobService.saveNewJob(newJobModel).subscribe({
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


  getSupervisors() {
    return this.employees.filter(x => x.employeeTypeId == EmployeeTypes.CrewSupervisor);
  }
}
