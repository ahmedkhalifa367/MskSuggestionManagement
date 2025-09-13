import { IEmployeeMskRecommendation } from "./IEmployeeMskRecommendation";
import { IMskRecommendation } from "./IMskRecommendation";

export interface IKanbanBoardMskRecommendation {
    vidaMskRecommendations: IMskRecommendation [],
    triggeredMskRecommendations: IEmployeeMskRecommendation[]
}