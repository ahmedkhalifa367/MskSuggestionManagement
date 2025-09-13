import { Status } from "@/models";

export interface IKanbanCard {
    Id: string;
    Title: string;
    Description: string;
    Level: string;
    Status: Status;
    EmployeeFullName: string | null;
    EmployeeId: string | null;
    RecommendationId: string;
}