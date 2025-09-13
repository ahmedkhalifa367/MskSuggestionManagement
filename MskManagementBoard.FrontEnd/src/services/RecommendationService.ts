import { MskManagementApi, mskManagementApi } from "@/api/MskManagementApi";
import { IApiUpdateStatusRequest, IEmployeeMskRecommendation } from "@/models/IEmployeeMskRecommendation";
import { IKanbanBoardMskRecommendation } from "@/models/IKanbanBoardMskRecommendation";
import { IApiAssignRecommendationRequest, IMskRecommendation } from "@/models/IMskRecommendation";

export class RecommendationService {
    constructor(private mskManagementApi: MskManagementApi) {}
    
    public async getMskRecommendations(): Promise<IKanbanBoardMskRecommendation> {
        return await mskManagementApi.Get<IKanbanBoardMskRecommendation>('/msk-recommendations');
    }
    public async getVidaMskRecommendations(): Promise<IMskRecommendation[]> {
        return await mskManagementApi.Get<IMskRecommendation[]>('/msk-recommendations/vida');
    }

    public async getEmployeeMskRecommendations(employeeId: string): Promise<IEmployeeMskRecommendation[]> {
        return mskManagementApi.Get<IEmployeeMskRecommendation[]>(
        `/msk-recommendations/${employeeId}/employee`
        );
    }

    public async assignMskRecommendation(request: IApiAssignRecommendationRequest): Promise<void> {
        return mskManagementApi.Post<IApiAssignRecommendationRequest, void>(
        '/msk-recommendations/assign',
        request
        );
    }

    public async updateMskRecommendationStatus(request: IApiUpdateStatusRequest): Promise<void> {
        return mskManagementApi.Put<IApiUpdateStatusRequest, void>(
        '/msk-recommendations/update-status',
        request
        );
    }
}

export const recommendationService = new RecommendationService(mskManagementApi);