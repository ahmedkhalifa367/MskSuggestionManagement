namespace MskSuggestionManagement.Application.Dtos
{
    public class EmployeeDto : IEmployeeDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

    }
}
