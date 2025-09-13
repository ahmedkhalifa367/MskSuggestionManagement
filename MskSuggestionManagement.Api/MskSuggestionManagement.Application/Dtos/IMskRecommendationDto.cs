namespace MskSuggestionManagement.Application.Dtos
{
    public interface IMskRecommendationDto
    {
        Guid Id { get; }
        string TypeDisplayName { get; }
        string LevelDisplayName { get; }
        string Description { get; }
    }
}
