namespace MskSuggestionManagement.Application.Dtos
{
    public interface IEmployeeMskRecommendationDto
    {
        IEmployeeDto Employee { get; }
        IMskRecommendationDto MskRecommendation { get; }
        string StatusDisplayName { get; }
    }
}
