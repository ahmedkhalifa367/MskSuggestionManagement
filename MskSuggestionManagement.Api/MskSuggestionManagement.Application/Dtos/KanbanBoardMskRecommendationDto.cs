namespace MskSuggestionManagement.Application.Dtos
{
    public class KanbanBoardMskRecommendationDto : IKanbanBoardMskRecommendationDto
    {
        public IEnumerable<IMskRecommendationDto> VidaMskRecommendations { get; set; } = Enumerable.Empty<IMskRecommendationDto>();
        public IEnumerable<IEmployeeMskRecommendationDto> TriggeredMskRecommendations { get; set; } = Enumerable.Empty<IEmployeeMskRecommendationDto>();
    }
}
