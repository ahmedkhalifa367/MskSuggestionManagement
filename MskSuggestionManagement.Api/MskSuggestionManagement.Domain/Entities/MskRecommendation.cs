namespace MskSuggestionManagement.Domain.Entities
{
    public class MskRecommendation : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public Enums.Type Type { get; set; }
        public Enums.Level Level { get; set; }
        public List<EmployeeMskRecommendation> EmployeeMskRecommendations { get; } = [];
    }
}
