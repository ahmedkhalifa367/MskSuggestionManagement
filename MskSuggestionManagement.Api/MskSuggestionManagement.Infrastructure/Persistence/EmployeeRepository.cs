using Microsoft.EntityFrameworkCore;
using MskSuggestionManagement.Domain.Entities;
using MskSuggestionManagement.Domain.Interfaces;
using MskSuggestionManagement.Infrastructure.DataManagement;

namespace MskSuggestionManagement.Infrastructure.Persistence
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MskManagementDbContext DbContext { get; set; }
        public EmployeeRepository(MskManagementDbContext context)
        {
            this.DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            this.DbContext.Employees.Add(employee);
            await this.DbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await this.DbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(Guid employeeId)
        {
            return await this.DbContext.Employees.SingleOrDefaultAsync(e => e.Id == employeeId);
        }
    }
}
