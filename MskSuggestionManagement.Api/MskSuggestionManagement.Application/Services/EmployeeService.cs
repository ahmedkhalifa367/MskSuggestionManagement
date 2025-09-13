using AutoMapper;
using MskSuggestionManagement.Application.Dtos;
using MskSuggestionManagement.Domain.Entities;
using MskSuggestionManagement.Domain.Interfaces;

namespace MskSuggestionManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository EmployeeRepository { get; set; }
        private IMapper Mapper { get; set; }
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.EmployeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<IEmployeeDto>> GetAllEmployees()
        {
            var employees = await this.EmployeeRepository.GetAllEmployees();
            return this.Mapper.Map<IEnumerable<EmployeeDto>>(employees);
        }

        public async Task<IEmployeeDto> AddEmployee(IEmployeeDto employeeDto)
        {
            // TODO: Need to handle the validation when we add teh employee
            var addedEmployee = await this.EmployeeRepository.AddEmployee(new Employee
            {
                Email = employeeDto.Email,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
            });

            return this.Mapper.Map<EmployeeDto>(addedEmployee);
        }
    }
}
