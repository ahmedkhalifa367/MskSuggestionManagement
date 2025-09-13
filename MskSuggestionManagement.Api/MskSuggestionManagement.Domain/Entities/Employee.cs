namespace MskSuggestionManagement.Domain.Entities
{
    public class Employee: BaseEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public List<EmployeeMskRecommendation> EmployeeMskRecommendations { get; } = [];
    }
}
