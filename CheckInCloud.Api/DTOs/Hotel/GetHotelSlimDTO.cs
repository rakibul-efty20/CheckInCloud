namespace CheckInCloud.Api.DTOs.Hotel;

public record GetHotelSlimDTO(
    int Id,
    string Name,
    string Address,
    double Rating
);