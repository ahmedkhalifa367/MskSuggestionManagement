using Microsoft.EntityFrameworkCore;
using MskSuggestionManagement.Domain.Entities;
using MskSuggestionManagement.Domain.Enums;
using MskSuggestionManagement.Domain.Interfaces;
using MskSuggestionManagement.Infrastructure.DataManagement;

namespace MskSuggestionManagement.Infrastructure.Persistence
{
    public class MskRecommendationRepository : IMskRecommendationRepository
    {
        private MskManagementDbContext DbContext { get; set; }
        public MskRecommendationRepository(MskManagementDbContext context)
        {
            this.DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<EmployeeMskRecommendation>> GetAllAsignedMskRecommendations()
        {
            return await this.DbContext.EmployeeMskRecommendations
                .Include(emr => emr.Employee)
                .Include(emr => emr.MskRecommendation)
                .ToListAsync();
        }

        public async Task<IEnumerable<MskRecommendation>> GetVidaMskRecommendations()
        {
            return await this.DbContext.MskRecommendations.ToListAsync();
        }

        public async Task<EmployeeMskRecommendation> AssignMskRecommendations(Guid employeeId, Guid mskRecommendationId)
        {
            // TODO: Move this in a seperate Repository this function should have one job asign MskRecommendation and validation should goes to the service
            await ValidateMskRecommendationNotAlreadyAssignedAsync(employeeId, mskRecommendationId);

            var employeeRecommendation = new EmployeeMskRecommendation
            {
                EmployeeId = employeeId,
                MskRecommendationId = mskRecommendationId,
                Status = Status.Assigned,
            };

            this.DbContext.EmployeeMskRecommendations.Add(employeeRecommendation);
            await this.DbContext.SaveChangesAsync();

            var assignedEmployeeMskRecommendation = await this.DbContext.EmployeeMskRecommendations
                .Include(er => er.Employee)
                .Include(er => er.MskRecommendation)
                .SingleOrDefaultAsync(er => er.EmployeeId == employeeId && er.MskRecommendationId == mskRecommendationId);

            return assignedEmployeeMskRecommendation;
        }

        public async Task<EmployeeMskRecommendation> UpdateMskRecommendationStatus(Guid employeeId, Guid mskRecommendationId, Status newStatus)
        {
            //TODO: Same here validation should be inside the service
            var employeeMskRecommendation = await EnsureEmployeeMskRecommendationExists(employeeId, mskRecommendationId);

            employeeMskRecommendation.Status = newStatus;
            await this.DbContext.SaveChangesAsync();

            return employeeMskRecommendation;
        }

        public async Task<IEnumerable<EmployeeMskRecommendation>> GetEmployeeMskRecommendations(Guid employeeId)
        {
            return await this.DbContext.EmployeeMskRecommendations
                .Include(er => er.Employee)
                .Include(er => er.MskRecommendation)
                .Where(er => er.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<MskRecommendation> GetMskRecommendationsById(Guid mskRecommendationId)
        {
            return await this.DbContext.MskRecommendations.SingleOrDefaultAsync(m => m.Id == mskRecommendationId);
        }

        private async Task<EmployeeMskRecommendation> EnsureEmployeeMskRecommendationExists(Guid employeeId, Guid mskRecommendationId)
        {
            var employeeMskRecommendation = await this.DbContext.EmployeeMskRecommendations
                .Include(er => er.MskRecommendation)
                .SingleOrDefaultAsync(er => er.EmployeeId == employeeId && er.MskRecommendationId == mskRecommendationId);

            if (employeeMskRecommendation is null)
                throw new ArgumentException(
                    $"No mskRecommendation assignment found for Employee ID {employeeId} and MskRecommendation ID {mskRecommendationId}");

            return employeeMskRecommendation;
        }

        private async Task ValidateMskRecommendationNotAlreadyAssignedAsync(Guid employeeId, Guid mskRecommendationId)
        {
            var existingAssignment = await this.DbContext.EmployeeMskRecommendations
                .SingleOrDefaultAsync(er => er.EmployeeId == employeeId && er.MskRecommendationId == mskRecommendationId);

            if (existingAssignment is not null)
                throw new InvalidOperationException("MskRecommendation already assigned to this employee");
        }
    }
}