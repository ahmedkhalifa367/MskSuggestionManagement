using MskSuggestionManagement.Domain.Entities;

namespace MskSuggestionManagement.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(Guid employeeId);
    }
}
