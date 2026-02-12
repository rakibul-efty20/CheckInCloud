namespace CheckInCloud.Api.DTOs.Hotel;

public record GetHotelDTO(
    int Id,
    string Name,
    string Address,
    double Rating,
    int CountryId,
    string Country
);

public record GetHotelSlimDTO(
    int Id,
    string Name,
    string Address,
    double Rating
);
