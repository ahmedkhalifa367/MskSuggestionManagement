using MskSuggestionManagement.Application.Dtos;

namespace MskSuggestionManagement.Application.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<IEmployeeDto>> GetAllEmployees();
        Task<IEmployeeDto> AddEmployee(IEmployeeDto employeeDto);
    }
}
