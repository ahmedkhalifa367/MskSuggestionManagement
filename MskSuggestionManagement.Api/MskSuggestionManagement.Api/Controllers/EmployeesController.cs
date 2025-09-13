using Microsoft.AspNetCore.Mvc;
using MskSuggestionManagement.Application.Dtos;
using MskSuggestionManagement.Application.Services;

namespace MskSuggestionManagement.Api.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeService EmployeeService { get; set; }

        public EmployeesController(IEmployeeService employeeService)
        {
            this.EmployeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }
        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A list of employees.</returns>
        [ProducesResponseType(typeof(IEnumerable<IEmployeeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IEmployeeDto>>> GetAllEmployees()
        {
            var employees = await this.EmployeeService.GetAllEmployees();
            return Ok(employees);
        }
        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="employeeDto">The employee details to create.</param>
        /// <returns>Created employee</returns>
        [ProducesResponseType(typeof(IEmployeeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<IEmployeeDto>> AddEmployee([FromBody] EmployeeDto employeeDto)
        {
            var addedEmployee = await this.EmployeeService.AddEmployee(employeeDto);
            return Ok(addedEmployee);
        }
    }
}
