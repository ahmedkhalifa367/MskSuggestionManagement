using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MskSuggestionManagement.Domain.Interfaces;
using MskSuggestionManagement.Infrastructure.DataManagement;
using MskSuggestionManagement.Infrastructure.Persistence;

namespace MskSuggestionManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<MskManagementDbContext>(options =>
            {
                options.UseInMemoryDatabase("MSKManagementDb");
                options.EnableDetailedErrors();
            });

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMskRecommendationRepository, MskRecommendationRepository>();

            return services;
        }
    }
}
