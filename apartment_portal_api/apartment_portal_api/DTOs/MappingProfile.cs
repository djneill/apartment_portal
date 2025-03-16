using apartment_portal_api.Models.Insights;
using apartment_portal_api.Models.IssueTypes;
using apartment_portal_api.Models.ParkingPermits;

namespace apartment_portal_api.DTOs
{
    using AutoMapper;
    using apartment_portal_api.Models;
    using apartment_portal_api.Models.Issues;
    using apartment_portal_api.Models.Guests;
    using apartment_portal_api.Models.Users;
    using apartment_portal_api.Models.UnitUsers;
    using apartment_portal_api.Models.Packages;
    using apartment_portal_api.Models.Statuses;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define mappings here
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<ApplicationUser, UserResponse>();
            CreateMap<RegistrationRequestDTO, ApplicationUser>();
            CreateMap<RegistrationForm, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<Unit, UnitDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));;
            CreateMap<UnitUserDTO, UnitUser>();
            CreateMap<Guest, GuestDTO>();
            CreateMap<GuestPostRequest, Guest>()
                .ForMember(
                    dest => dest.Expiration,
                    opt => opt.MapFrom(b => DateTime.UtcNow.AddHours(b.DurationInHours)));
            CreateMap<ParkingPermitPostRequest, ParkingPermit>();
            CreateMap<ParkingPermit, ParkingPermitDTO>();
            CreateMap<Package, PackageGetByIdResponse>();
            CreateMap<Package, PackageGetResponse>();
            CreateMap<Status, StatusResponse>();
            CreateMap<Status, StatusDTO>();
            CreateMap<Issue, IssueResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.ApplicationUser));
            CreateMap<IssueType, IssueTypeResponse>();
            CreateMap<Insight, InsightResponse>();
            CreateMap<InsightPostRequest, Insight>();

            CreateMap<Issue, IssueAIPostRequest>();
        }
    }
}