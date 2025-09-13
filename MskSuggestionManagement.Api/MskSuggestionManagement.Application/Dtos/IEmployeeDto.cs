namespace MskSuggestionManagement.Application.Dtos
{
    public interface IEmployeeDto
    {
        Guid Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string FullName { get; }

    }
}
