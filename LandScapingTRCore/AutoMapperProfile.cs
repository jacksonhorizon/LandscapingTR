using AutoMapper;
using LandscapingTR.Core.Entities.CompanyResources;
using LandscapingTR.Core.Entities.Domain;
using LandscapingTR.Core.Entities.Lookups;
using LandscapingTR.Core.Entities.Time;
using LandscapingTR.Core.Models.CompanyResources;
using LandscapingTR.Core.Models.Domain;
using LandscapingTR.Core.Models.Lookups;
using LandscapingTR.Core.Models.Time;

namespace LandscapingTR.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<JobType, LookupItemModel>()
                .ForMember(dest => dest.LookupValue, opt => opt.MapFrom(src => src.JobTypeDisplayValue))
                .ReverseMap();
            CreateMap<CustomerType, LookupItemModel>()
                .ForMember(dest => dest.LookupValue, opt => opt.MapFrom(src => src.CustomerTypeDisplayValue))
                .ReverseMap();
            CreateMap<LocationType, LookupItemModel>()
                .ForMember(dest => dest.LookupValue, opt => opt.MapFrom(src => src.LocationTypeDisplayValue))
                .ReverseMap();
            CreateMap<EmployeeType, LookupItemModel>()
                .ForMember(dest => dest.LookupValue, opt => opt.MapFrom(src => src.EmployeeTypeDisplayValue))
                .ReverseMap();
            CreateMap<TimeEntry, TimeEntryModel>()
                .ReverseMap();
            CreateMap<TimeEntryHistory, TimeEntryModel>()
                .ReverseMap();
            CreateMap<Employee, EmployeeModel>()
               .ReverseMap();
            CreateMap<Job, JobModel>()
               .ReverseMap();
            CreateMap<Location, LocationModel>()
               .ReverseMap();
        }
    }
}
