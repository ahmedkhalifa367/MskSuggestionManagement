using Microsoft.Extensions.DependencyInjection;
using MskSuggestionManagement.Application.Mappings;
using MskSuggestionManagement.Application.Services;

namespace MskSuggestionManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMskRecommendationService, MskRecommendationService>();

            return services;
        }
    }
}
