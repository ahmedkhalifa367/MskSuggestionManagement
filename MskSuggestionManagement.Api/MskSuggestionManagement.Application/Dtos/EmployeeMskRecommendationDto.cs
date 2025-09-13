using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Application.Dtos
{
    public class EmployeeMskRecommendationDto : IEmployeeMskRecommendationDto
    {
        public string StatusDisplayName { get; set; }
        public Status Status { get; set; }
        public IEmployeeDto Employee { get; set; }
        public IMskRecommendationDto MskRecommendation { get; set; }

    }
}
