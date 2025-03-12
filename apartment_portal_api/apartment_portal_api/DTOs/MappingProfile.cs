using apartment_portal_api.Models.ParkingPermits;

namespace apartment_portal_api.DTOs
{
    using AutoMapper;
    using apartment_portal_api.Models;
    using apartment_portal_api.Models.Issues;
    using apartment_portal_api.Models.Guests;
    using apartment_portal_api.Models.Users;
    using apartment_portal_api.Models.Packages;
    using apartment_portal_api.Models.Statuses;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define mappings here
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<RegistrationRequestDTO, ApplicationUser>();
            CreateMap<Unit, UnitDTO>();
            CreateMap<Guest, GuestDTO>();
            CreateMap<GuestPostRequest, Guest>()
                .ForMember(
                    dest => dest.Expiration,
                    opt => opt.MapFrom(b => DateTime.UtcNow.AddHours(b.DurationInHours)));
            CreateMap<ParkingPermitPostRequest, ParkingPermit>();
            CreateMap<Issue, IssueDTO>();
            CreateMap<Package, PackageGetResponse>();
            CreateMap<Status, StatusResponse>();
        }
    }
}