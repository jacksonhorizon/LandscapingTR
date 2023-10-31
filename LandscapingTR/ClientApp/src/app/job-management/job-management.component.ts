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
import { ConfirmationDialogService } from '../core/components/confirmation-dialog/confirmation-dialog.service';

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

  employees: EmployeeModel[] = [];
  jobs: JobModel[] = [];
  jobTypes!: LookupItemModel[];

  constructor(private route: ActivatedRoute,
    private employeeService: EmployeeService,
    private lookupService: LookupService,
    private jobService: JobService,
    private confirmationDialogService: ConfirmationDialogService,
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
      this.jobService.getAllJobs(),
      this.employeeService.getAllEmployees()
    ]).subscribe({
      next: data => {
        // data is an array containing the results of the observables in the same order
        this.employeeModel = data[0];
        this.jobTypes = data[1];
        this.jobs = data[2];
        this.employees = data[3];
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

      return formatDate(dateToFormat, "MM-dd-yyyy", 'en-US');
    }

    return dateToFormat;
  }

  getJobStatus(isCompleted: boolean | undefined, inProgress: boolean | undefined) {
    if (isCompleted != undefined && inProgress != undefined) {



      // call to see if there are any jobs in progress
      if (isCompleted) {

        return "Completed";
      }
      if (inProgress) {
        return "In Progress";
      }

      return "New";
    }
    

    return "New";
  }

  getEmployeeName(employeeId: number | undefined) {
    if (employeeId != undefined) {

      var employee = this.employees.find(x => x.id === employeeId);

      if (employee === null) {
        return "";
      }
      else {
        return employee?.lastName + ", " + employee?.firstName;
      }
    }

    return employeeId;
  }

  getNumberOfEmployees(job: JobModel) {
    var count = 0;
    if (job.firstCrewMemberId !== null) {
      count += 1;
    }
    if (job.secondCrewMemberId !== null) {
      count += 1;
    }
    if (job.thirdCrewMemberId !== null) {
      count += 1;
    }
    if (job.fourthCrewMemberId !== null) {
      count += 1;
    }
    if (job.crewSupervisorId !== null) {
      count += 1;
    }
    if (job.equipmentAndSafetyOfficerId !== null) {
      count += 1;
    }
    if (job.landscapeDesignerId !== null) {
      count += 1;
    }

    return count;
  }

  rerouteToAddJobPage(data: EmployeeModel): void {
    this.router.navigate(["job-add/:" + data.id])
  }

  rerouteToEditJobPage(data: EmployeeModel, jobClicked: JobModel): void {
    this.router.navigate(["job-edit/:" + data.id + "/:" + jobClicked.id]);
  }

  deleteJob(jobClicked: EmployeeModel) {
    this.confirmationDialogService.confirm('Please confirm:', 'Are you sure you want to delete this job?')
      .then((confirmed) => {
        console.log('User confirmed:', confirmed)

        if (confirmed) {
          this.jobService.deleteJob(jobClicked).subscribe({
            next: data => {
              this.toastr.success('Job was deleted successfully!', 'Delete Job: ');

              location.reload();
            },
            error: err => {
              console.log(err);
            }
          });
        }
      })
      .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }
}
