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
            CreateMap<Issue, IssueDTO>();
            CreateMap<Package, PackageDTO>();
            CreateMap<Status, StatusDTO>();
        }
    }
}