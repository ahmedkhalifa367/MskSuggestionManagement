using AutoMapper;
using MskSuggestionManagement.Application.Dtos;
using MskSuggestionManagement.Domain.Entities;
using MskSuggestionManagement.Domain.Enums;

namespace MskSuggestionManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}".Trim()));
            CreateMap<Employee, IEmployeeDto>().As<EmployeeDto>();

            CreateMap<MskRecommendation, MskRecommendationDto>()
                .ForMember(dest => dest.TypeDisplayName, opt => opt.MapFrom(src => GetTypeDisplayName(src.Type)))
                .ForMember(dest => dest.LevelDisplayName, opt => opt.MapFrom(src => GetLevelDisplayName(src.Level)));
            CreateMap<MskRecommendation, IMskRecommendationDto>().As<MskRecommendationDto>();

            CreateMap<EmployeeMskRecommendation, EmployeeMskRecommendationDto>()
                .ForMember(dest => dest.StatusDisplayName, opt => opt.MapFrom(src => GetStatusDisplayName(src.Status)));
            CreateMap<EmployeeMskRecommendation, IEmployeeMskRecommendationDto>().As<EmployeeMskRecommendationDto>();
        }

        private static string GetTypeDisplayName(Domain.Enums.Type type) => type switch
        {
            Domain.Enums.Type.TargetedExercise => "Targeted Exercise",
            Domain.Enums.Type.WorkspaceAdjustment => "Workspace Adjustment / Equipment orders",
            Domain.Enums.Type.BehavioralChange => "Behavioral Change",
            Domain.Enums.Type.LifestyleChange => "Lifestyle Change",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unsupported msk recomndation type value.")
        };

        private static string GetLevelDisplayName(Level level) => level switch
        {
            Level.Low => "Low",
            Level.Medium => "Medium",
            Level.High => "High",
            _ => throw new ArgumentOutOfRangeException(nameof(level), level, "Unsupported msk recomndation level value.")
        };

        private static string GetStatusDisplayName(Status status) => status switch
        {
            Status.New => "New",
            Status.Assigned => "Assigned",
            Status.InProgress => "InProgress",
            Status.Completed => "Completed",
            Status.Rejected => "Rejected",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unsupported msk recomndation status value.")
        };
    }
}
