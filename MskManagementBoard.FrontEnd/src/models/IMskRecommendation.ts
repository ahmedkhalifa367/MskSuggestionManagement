import { RiskLevel } from "./RiskLevel";

export interface IMskRecommendation {
  id: string;
  typeDisplayName: string;
  levelDisplayName: RiskLevel;
  description: string;
}

export interface IApiAssignRecommendationRequest {
  employeeId: string;
  mskRecommendationId: string;
}