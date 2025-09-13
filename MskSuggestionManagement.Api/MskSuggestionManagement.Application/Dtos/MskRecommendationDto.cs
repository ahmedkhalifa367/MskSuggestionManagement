using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Application.Dtos
{
    public class MskRecommendationDto : IMskRecommendationDto
    {
        public Guid Id { get; set; }
        public string TypeDisplayName { get; set; }
        public Domain.Enums.Type Type { get; set; }
        public string LevelDisplayName { get; set; }
        public Level Level { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
