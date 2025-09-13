import { IEmployee } from './IEmployee';
import { IMskRecommendation } from './IMskRecommendation';
import { Status } from './Status';

export interface IEmployeeMskRecommendation {
  employee: IEmployee;
  mskRecommendation: IMskRecommendation;
  statusDisplayName: Status;
}

export interface IApiUpdateStatusRequest {
  employeeId: string;
  mskRecommendationId: string;
  newStatus: Status;
}