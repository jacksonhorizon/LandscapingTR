export class JobModel{
  Id?: number;
  JobTypeId?: number;
  JobDate?: Date;
  LocationId?: number;
  FirstCrewMemberId?: number;
  SecondCrewMemberId?: number;
  ThirdCrewMemberId?: number;
  FourthCrewMemberId?: number;
  CrewSupervisorId?: number;
  LandscapeDesignerId?: number;
  EquipmentAndSafetyOfficerId?: number;
  EstimatedTotalHours?: number;
  TotalLoggedHours?: number;
  isCompleted?: boolean;
}
