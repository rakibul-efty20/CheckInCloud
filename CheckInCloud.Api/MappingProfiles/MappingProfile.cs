using AutoMapper;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DTOs.Country;
using CheckInCloud.Api.DTOs.Hotel;

namespace CheckInCloud.Api.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //for the hotel mapping
            CreateMap<Hotel, GetHotelDTO>()
                .ForMember(d => d.Country, cfg => cfg.MapFrom(src => src.Country!.Name)); 
            CreateMap<CreateHotelDTO, Hotel>();

            CreateMap<Hotel, GetHotelSlimDTO>(); // Added for Country -> GetCountryDto nested projection


            //for the country mapping
            CreateMap<Country, GetCountryDTO>()
                .ForMember(d => d.CountryId, opt => opt.MapFrom(s => s.CountryId));
            CreateMap<Country, GetCountriesDTO>()
                .ForMember(d => d.CountryId, opt => opt.MapFrom(s => s.CountryId));
            CreateMap<CreateCountryDTO, Country>();
        }
    }
}
