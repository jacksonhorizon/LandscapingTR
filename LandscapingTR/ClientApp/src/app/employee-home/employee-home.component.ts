import { Component } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-employee-home',
  styleUrls: ['./employee-home.component.css'],
  templateUrl: './employee-home.component.html',
})
export class EmployeeHomeComponent {
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  // General properties
  

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    // Gets the employee Id
    const employeeIdFromUrl = this.route.snapshot.paramMap.get('id')?.slice(1);
    if (employeeIdFromUrl == null) {
      this.employeeId = -1;
    } else {
      this.employeeId = +employeeIdFromUrl;
    }

    this.pathEmployeeId = ":" + this.employeeId.toString();
  }

  // Is for the header
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  // General methodss.isExpanded = !this.isExpanded;
}
