namespace MskSuggestionManagement.Api.Dtos.Requests
{
    public class ApiAssignMskRecommendationRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid MskRecommendationId { get; set; }
    }
}
