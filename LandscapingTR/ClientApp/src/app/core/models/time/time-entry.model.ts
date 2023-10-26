export class TimeEntryModel {
  id?: number;
  employeeId?: number
  entryDate?: Date;
  employeeTypeId?: number;
  jobTypeId?: number;
  jobId?: number;
  totalLoggedHours?: number;
  lastModifiedDate?: Date;
  isSubmitted?: boolean = false;
  isApproved = false;
}
