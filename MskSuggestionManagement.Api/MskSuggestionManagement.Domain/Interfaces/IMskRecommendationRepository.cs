using MskSuggestionManagement.Domain.Entities;
using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Domain.Interfaces
{
    public interface IMskRecommendationRepository
    {
        Task<IEnumerable<MskRecommendation>> GetVidaMskRecommendations();
        Task<IEnumerable<EmployeeMskRecommendation>> GetAllAsignedMskRecommendations();
        Task<EmployeeMskRecommendation> AssignMskRecommendations(Guid employeeId, Guid mskRecommendationId);
        Task<EmployeeMskRecommendation> UpdateMskRecommendationStatus(Guid employeeId, Guid mskRecommendationId, Status newStatus);
        Task<IEnumerable<EmployeeMskRecommendation>> GetEmployeeMskRecommendations(Guid employeeId);
        Task<MskRecommendation> GetMskRecommendationsById(Guid mskRecommendationId);
    }
}
