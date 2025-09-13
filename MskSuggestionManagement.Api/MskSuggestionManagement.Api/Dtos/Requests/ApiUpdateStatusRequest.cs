using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Api.Dtos.Requests
{
    public class ApiUpdateStatusRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid MskRecommendationId { get; set; }
        public Status NewStatus { get; set; }
    }
}
