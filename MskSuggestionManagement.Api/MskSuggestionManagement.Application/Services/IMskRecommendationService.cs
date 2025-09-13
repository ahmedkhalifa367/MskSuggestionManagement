using MskSuggestionManagement.Application.Dtos;
using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Application.Services
{
    public interface IMskRecommendationService
    {
        Task<IKanbanBoardMskRecommendationDto> GetAllMskRecommendations();
        Task<IEnumerable<IMskRecommendationDto>> GetVidaMskRecommendations();
        Task<IEmployeeMskRecommendationDto> AssignMskRecommendation(Guid employeeId, Guid mskRecommendationId);
        Task<IEmployeeMskRecommendationDto> UpdateMskRecommendationStatus(Guid employeeId, Guid mskRecommendationId, Status newStatus);
        Task<IEnumerable<IEmployeeMskRecommendationDto>> GetAllEmployeeMskRecommendations(Guid employeeId);
    }
}
