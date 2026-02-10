namespace CheckInCloud.Api.DTOs.Hotel;

public record GetHotelsDTO(
    int Id,
    string Name,
    string Address,
    double Rating,
    int CountryId
);