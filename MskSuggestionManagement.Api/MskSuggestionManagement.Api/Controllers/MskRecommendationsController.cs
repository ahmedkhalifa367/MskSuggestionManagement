using Microsoft.AspNetCore.Mvc;
using MskSuggestionManagement.Api.Dtos.Requests;
using MskSuggestionManagement.Application.Dtos;
using MskSuggestionManagement.Application.Services;

namespace MskSuggestionManagement.Api.Controllers
{
    [Route("api/msk-recommendations")]
    [ApiController]
    public class MskRecommendationsController : ControllerBase
    {
        private IMskRecommendationService MskRecommendationService { get; set; }

        public MskRecommendationsController(IMskRecommendationService mskRecommendationService)
        {
            this.MskRecommendationService = mskRecommendationService;
        }
        /// <summary>
        /// Retrieves all MSK recommendations for the Kanban board,
        /// including employee-assigned items and unassigned items sourced from VIDA.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IKanbanBoardMskRecommendationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet()]
        public async Task<ActionResult<IKanbanBoardMskRecommendationDto>> GetAllMskRecommendations()
        {
            var mskRecommendations = await this.MskRecommendationService.GetAllMskRecommendations();
            return Ok(mskRecommendations);
        }
        /// <summary>
        /// Retrieves unassigned MSK recommendations sourced from VIDA.
        /// </summary>
        /// <returns>A list of MSK recommendations.</returns>
        [ProducesResponseType(typeof(IEnumerable<IMskRecommendationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("vida")]
        public async Task<ActionResult<IEnumerable<IMskRecommendationDto>>> GetVidaMskRecommendations()
        {
            var mskRecommendations = await this.MskRecommendationService.GetVidaMskRecommendations();
            return Ok(mskRecommendations);
        }
        /// <summary>
        /// Retrieves all MSK recommendations for a specific employee.
        /// </summary>
        /// <param name="employeeId">The employee id</param>
        /// <returns>A list of MSK recommendations for the specified employee.</returns>
        [ProducesResponseType(typeof(IEnumerable<IEmployeeMskRecommendationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{employeeId}/employee")]
        public async Task<ActionResult<IEnumerable<IEmployeeMskRecommendationDto>>> GetAllEmployeeMskRecommendations(Guid employeeId)
        {
            var assignments = await this.MskRecommendationService.GetAllEmployeeMskRecommendations(employeeId);
            return Ok(assignments);
        }
        /// <summary>
        /// Assigns an MSK recommendation to an employee.
        /// </summary>
        /// <param name="request">The employee and recommendation identifiers.</param>
        [ProducesResponseType(typeof(IEmployeeMskRecommendationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("assign")]
        public async Task<ActionResult> AssignRecommendation([FromBody] ApiAssignMskRecommendationRequest request)
        {

            var assignedEmployeeMskRecommendation = await this.MskRecommendationService.AssignMskRecommendation(request.EmployeeId, request.MskRecommendationId);
            return Ok(assignedEmployeeMskRecommendation);
        }
        /// <summary>
        /// Updates the status of an employee–MSK recommendation assignment.
        /// </summary>
        /// <param name="request">The composite key (EmployeeId, MskRecommendationId) and the new status value.</param>
        /// <returns>The updated MSK recommendation.</returns>
        [ProducesResponseType(typeof(IMskRecommendationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("update-status")]
        public async Task<ActionResult<IMskRecommendationDto>> UpdateStatus([FromBody] ApiUpdateStatusRequest request)
        {
            var recommendation = await this.MskRecommendationService.UpdateMskRecommendationStatus(request.EmployeeId, request.MskRecommendationId, request.NewStatus);
            return Ok(recommendation);
        }
    }
}
