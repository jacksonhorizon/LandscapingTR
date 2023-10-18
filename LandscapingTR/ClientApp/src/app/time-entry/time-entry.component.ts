import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { TimeEntryModel } from '../core/models/time/time-entry.model';

@Component({
  selector: 'app-time-entry',
  styleUrls: ['./time-entry.component.css'],
  templateUrl: './time-entry.component.html'
})
export class TimeEntryComponent {
  // The employee Id as a number/string
  employeeId!: number;
  pathEmployeeId!: string;

  // General properties
  timeEntries: TimeEntryModel[] = [];

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

  // General methods
}
