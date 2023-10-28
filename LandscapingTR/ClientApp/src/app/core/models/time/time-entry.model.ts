export class TimeEntryModel {
  id?: number;
  employeeId?: number | null;
  entryDate?: Date;
  employeeTypeId?: number | null;
  jobTypeId?: number | null;
  jobId?: number | null;
  totalLoggedHours?: number;
  lastModifiedDate?: Date;
  isSubmitted?: boolean = false;
  isApproved = false;
}
