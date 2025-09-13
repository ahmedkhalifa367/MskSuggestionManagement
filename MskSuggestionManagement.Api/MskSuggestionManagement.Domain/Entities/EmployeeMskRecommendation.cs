using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Domain.Entities
{
    public class EmployeeMskRecommendation
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public Guid MskRecommendationId { get; set; }
        public MskRecommendation MskRecommendation { get; set; }

        public Status Status { get; set; } = Status.New;
    }
}
