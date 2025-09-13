using AutoMapper;
using MskSuggestionManagement.Application.Common.Exceptions;
using MskSuggestionManagement.Application.Dtos;
using MskSuggestionManagement.Domain.Entities;
using MskSuggestionManagement.Domain.Enums;
using MskSuggestionManagement.Domain.Interfaces;

namespace MskSuggestionManagement.Application.Services
{
    public class MskRecommendationService : IMskRecommendationService
    {
        private IMskRecommendationRepository MskRecommendationRepository { get; set; }
        private IEmployeeRepository EmployeeRepository { get; set; }
        private IMapper Mapper { get; set; }

        public MskRecommendationService(IMskRecommendationRepository mskRecommendationRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.MskRecommendationRepository = mskRecommendationRepository ?? throw new ArgumentNullException(nameof(mskRecommendationRepository));
            this.EmployeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            this.Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IKanbanBoardMskRecommendationDto> GetAllMskRecommendations()
        {
            var vidaMskRecommendations = await this.MskRecommendationRepository.GetVidaMskRecommendations();
            var assignedMskRecommendations = await this.MskRecommendationRepository.GetAllAsignedMskRecommendations();

            return new KanbanBoardMskRecommendationDto
            {
                VidaMskRecommendations = this.Mapper.Map<IEnumerable<MskRecommendationDto>>(vidaMskRecommendations),
                TriggeredMskRecommendations = this.Mapper.Map<IEnumerable<EmployeeMskRecommendationDto>>(assignedMskRecommendations)
            };
        }

        public async Task<IEnumerable<IMskRecommendationDto>> GetVidaMskRecommendations()
        {
            var mskRecommendations = await this.MskRecommendationRepository.GetVidaMskRecommendations();
            return this.Mapper.Map<IEnumerable<MskRecommendationDto>>(mskRecommendations);
        }

        public async Task<IEmployeeMskRecommendationDto> AssignMskRecommendation(Guid employeeId, Guid recommendationId)
        {

            var employee = await this.EmployeeRepository.GetEmployeeById(employeeId);
            if (employee is null)
                throw new NotFoundException($"Employee with ID {employeeId} not found");

            var assignedEmployeeMskRecommendation = await this.MskRecommendationRepository.AssignMskRecommendations(employeeId, recommendationId);

            return this.Mapper.Map<EmployeeMskRecommendationDto>(assignedEmployeeMskRecommendation);
        }

        public async Task<IEmployeeMskRecommendationDto> UpdateMskRecommendationStatus(Guid employeeId, Guid recommendationId, Status newStatus)
        {
            var employee = await this.EmployeeRepository.GetEmployeeById(employeeId);
            if (employee is null)
                throw new NotFoundException($"Employee with ID {employeeId} not found");

            var mskRecommendation = await this.MskRecommendationRepository.UpdateMskRecommendationStatus(employeeId, recommendationId, newStatus);

            return this.Mapper.Map<EmployeeMskRecommendationDto>(mskRecommendation);
        }

        public async Task<IEnumerable<IEmployeeMskRecommendationDto>> GetAllEmployeeMskRecommendations(Guid employeeId)
        {
            var employeeRecommendations = await this.MskRecommendationRepository.GetEmployeeMskRecommendations(employeeId);
            return this.Mapper.Map<IEnumerable<EmployeeMskRecommendationDto>>(employeeRecommendations);
        }
    }
}
