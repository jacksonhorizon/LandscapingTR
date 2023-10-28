export class JobModel{
  id?: number;
  jobName?: string;
  jobTypeId?: number;
  jobDate?: Date;
  locationId?: number;
  firstCrewMemberId?: number;
  secondCrewMemberId?: number;
  thirdCrewMemberId?: number;
  fourthCrewMemberId?: number;
  crewSupervisorId?: number;
  landscapeDesignerId?: number;
  equipmentAndSafetyOfficerId?: number;
  estimatedTotalHours?: number;
  totalLoggedHours?: number;
  isCompleted?: boolean;
}
