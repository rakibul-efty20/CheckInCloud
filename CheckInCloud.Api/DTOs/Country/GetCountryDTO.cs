using CheckInCloud.Api.DTOs.Hotel;

namespace CheckInCloud.Api.DTOs.Country;

public record GetCountryDTO(
    int CountryId,
    string Name,
    string ShortName,
    List<GetHotelSlimDTO>? Hotels
);