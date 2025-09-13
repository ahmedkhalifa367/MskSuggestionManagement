namespace MskSuggestionManagement.Application.Dtos
{
    public interface IKanbanBoardMskRecommendationDto
    {
        IEnumerable<IMskRecommendationDto> VidaMskRecommendations { get; }
        IEnumerable<IEmployeeMskRecommendationDto> TriggeredMskRecommendations { get; }
    }
}
