using Microsoft.EntityFrameworkCore;
using MskSuggestionManagement.Domain.Entities;
using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Infrastructure.DataManagement
{
    public static class SeedData
    {
        public static async Task SeedAsync(MskManagementDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Employees.AnyAsync())
                await context.Employees.AddRangeAsync(Employees);

            if (!await context.MskRecommendations.AnyAsync())
                await context.MskRecommendations.AddRangeAsync(MskRecommendations);

            await context.SaveChangesAsync();
        }

        public static readonly List<Employee> Employees = new()
        {
            new Employee { FirstName = "Ahmed", LastName = "Khalifa", Email = "ahmedkhalifa367@gmail.com" },
            new Employee { FirstName = "Assem", LastName = "Mohamed", Email = "assem.mohamed@gmail.com" },
            new Employee { FirstName = "Mona", LastName = "Sayed", Email = "mona.Sayed@gmail.com" },
            new Employee { FirstName = "David", LastName = "Wilson", Email = "david.wilson@gmail.com" },
            new Employee { FirstName = "Lisa", LastName = "Anderson", Email = "lisa.anderson@gmail.com" },
            new Employee { FirstName = "Tom", LastName = "Taylor", Email = "tom.taylor@gmail.com" },
            new Employee { FirstName = "Anna", LastName = "Thomas", Email = "anna.thomas@gmail.com" }
        };

        public static readonly List<MskRecommendation> MskRecommendations = new()
        {
            new MskRecommendation
            {
                Type = Domain.Enums.Type.TargetedExercise,
                Level = Level.Medium,
                Title = "Targeted Exercise",
                Description = "Neck and shoulder stretches, Perform gentle neck rolls and shoulder shrugs every hour",
            },
            new MskRecommendation
            {
                Type = Domain.Enums.Type.TargetedExercise,
                Level = Level.High,
                Title = "Targeted Exercise",
                Description = "Lower back strengthening exercises, Perform 10-15 back extensions and cat-cow stretches daily",
            },
            new MskRecommendation
            {
                Type = Domain.Enums.Type.WorkspaceAdjustment,
                Level = Level.Low,
                Title = "Workspace Adjustments",
                Description = "Monitor height adjustment, Adjust monitor so top of screen is at eye level",
            },
            new MskRecommendation
            {
                Type = Domain.Enums.Type.WorkspaceAdjustment,
                Level = Level.Medium,
                Title = "Workspace Adjustments",
                Description = "Ergonomic keyboard and mouse setup, Order external keyboard and mouse for laptop users",
            },
            new MskRecommendation
            {
                Type = Domain.Enums.Type.BehavioralChange,
                Level = Level.Low,
                Title = "Behavioral Changes",
                Description = "Take micro-breaks every 30 minutes, Stand up and walk around for 2-3 minutes every 30 minutes",
            },
            new MskRecommendation
            {
                Type = Domain.Enums.Type.BehavioralChange,
                Level = Level.Medium,
                Title = "Behavioral Changes",
                Description = "Practice proper sitting posture, Keep feet flat on floor, back straight, shoulders relaxed",
            },
            new MskRecommendation
            {
                Type = Domain.Enums.Type.LifestyleChange,
                Level = Level.High,
                Title = "Lifestyle Changes",
                Description = "Improve sleep schedule, Maintain consistent sleep schedule with 7-8 hours nightly",
            },
            new MskRecommendation
            {
                Type = Domain.Enums.Type.LifestyleChange,
                Level = Level.Medium,
                Title = "Lifestyle Changes",
                Description = "Regular physical activity, Engage in 30 minutes of moderate exercise 3-4 times per week",
            }
        };
    }
}