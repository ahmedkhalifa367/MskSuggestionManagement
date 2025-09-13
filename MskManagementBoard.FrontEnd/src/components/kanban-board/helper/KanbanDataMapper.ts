import { IEmployeeMskRecommendation } from "@/models/IEmployeeMskRecommendation";
import { IMskRecommendation } from "@/models/IMskRecommendation";
import { IKanbanCard } from "../models";

export class KanbanDataMapper {
  /*
    Kanban Data rules
    Title of the card must be unique
  */
  private static cardCounter = 0;
  private static getNextCardNumber(): number {
    return ++this.cardCounter;
  }
  public static mapEmployeeMskRecommendation(
    { employee, mskRecommendation, statusDisplayName }: IEmployeeMskRecommendation
  ): IKanbanCard {
    return {
      Id: `${employee.id}-${mskRecommendation.id}`,
      Title: `#${this.getNextCardNumber()} ${mskRecommendation.typeDisplayName}`,
      Description: mskRecommendation.description,
      Level: mskRecommendation.levelDisplayName,
      Status: statusDisplayName,
      EmployeeFullName: employee.fullName,
      EmployeeId: employee.id,
      RecommendationId: mskRecommendation.id,
    };
  }
  
  public static mapEmployeeMskRecommendations(mskRecommendations: IEmployeeMskRecommendation[]): IKanbanCard[] {
    return mskRecommendations.map(er => this.mapEmployeeMskRecommendation(er));
  }

  public static mapVidaMskRecommendation(mskRecommendation: IMskRecommendation): IKanbanCard {
    return {
      Id: mskRecommendation.id,
      Title: `#${this.getNextCardNumber()} ${mskRecommendation.typeDisplayName}`,
      Description: mskRecommendation.description,
      Level: mskRecommendation.levelDisplayName,
      Status: "New",
      EmployeeId: null,
      EmployeeFullName: null,
      RecommendationId: mskRecommendation.id,
    };
  }

  public static mapVidaMskRecommendations(mskRecommendations: IMskRecommendation[]): IKanbanCard[] {
    return mskRecommendations.map(er => this.mapVidaMskRecommendation(er));
  }
}
