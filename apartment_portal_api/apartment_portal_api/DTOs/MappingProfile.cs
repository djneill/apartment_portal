using apartment_portal_api.Models;
using apartment_portal_api.Models.Guests;
using apartment_portal_api.Models.Insights;
using apartment_portal_api.Models.Issues;
using apartment_portal_api.Models.IssueTypes;
using apartment_portal_api.Models.Packages;
using apartment_portal_api.Models.ParkingPermits;
using apartment_portal_api.Models.Statuses;
using apartment_portal_api.Models.UnitUsers;
using apartment_portal_api.Models.Users;
using AutoMapper;

namespace apartment_portal_api.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<ApplicationUser, UserResponse>();
            CreateMap<RegistrationRequestDTO, ApplicationUser>();
            CreateMap<RegistrationForm, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<ApplicationUser, GetUsersResponse>()
                .ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.UnitUserUsers.FirstOrDefault().Unit));
            CreateMap<Unit, GetUsersUnitResponse>(); // Used in GetUsers fetch request on UserController

            // Unit
            CreateMap<Unit, UnitDTO>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name));;
            CreateMap<UnitUserDTO, UnitUser>();

            // Guest
            CreateMap<Guest, GuestDTO>();
            CreateMap<GuestPostRequest, Guest>()
                .ForMember(
                    dest => dest.Expiration,
                    opt => opt.MapFrom(b => DateTime.UtcNow.AddHours(b.DurationInHours)));
            CreateMap<ParkingPermitPostRequest, ParkingPermit>();
            CreateMap<ParkingPermit, ParkingPermitDTO>();

            // Package
            CreateMap<Package, PackageGetByIdResponse>();
            CreateMap<Package, PackageGetResponse>();

            // Status
            CreateMap<Status, StatusResponse>();
            CreateMap<Status, StatusDTO>();
            CreateMap<StatusPostRequest, Status>();

            // Issue
            CreateMap<Issue, IssueResponse>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.ApplicationUser));
            CreateMap<IssueType, IssueTypeResponse>();

            // Insight
            CreateMap<Insight, InsightResponse>();
            CreateMap<InsightPostRequest, Insight>();

            CreateMap<Issue, IssueAIPostRequest>();
        }
    }
}