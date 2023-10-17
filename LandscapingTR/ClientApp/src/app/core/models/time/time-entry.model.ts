export class TimeEntryModel {
  Id?: number;
  EmployeeId?: number
  EntryDate?: Date;
  EmployeeTypeId?: number;
  JobTypeId?: number;
  JobId?: number;
  TotalLoggedHours?: number;
  LastModifiedDate?: Date;
  IsSubmitted?: boolean = false;
  IsApproved: boolean = false;
}
