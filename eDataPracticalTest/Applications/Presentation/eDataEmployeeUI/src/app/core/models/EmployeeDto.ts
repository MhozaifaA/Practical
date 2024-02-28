export interface EmployeeDto {
  id?: string;
  firstName?: string;
  lastName?: string;
  dateOfBirth?: string;
  status?: EmployeeStatus;
  departmentId?: string;
  departmentName?: string | null;
}


export enum EmployeeStatus {
  Active,
  UnderTrial,
  Unemployed,
  Cancelled,
  Blocked
}
