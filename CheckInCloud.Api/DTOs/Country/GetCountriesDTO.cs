namespace CheckInCloud.Api.DTOs.Country;

public record GetCountriesDTO(
    int CountryId,
    string Name,
    string ShortName
);