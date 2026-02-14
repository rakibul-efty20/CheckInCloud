using CheckInCloud.Api.DTOs.Hotel;
using CheckInCloud.Api.Results;
namespace HotelListing.Api.Contracts;

public interface IHotelsService
{
    // Keep these for quick checks elsewhere if needed
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name, int countryId);
    Task<Result<IEnumerable<GetHotelDTO>>> GetHotelsAsync();
    Task<Result<GetHotelDTO>> GetHotelAsync(int id);
    Task<Result<GetHotelDTO>> CreateHotelAsync(CreateHotelDTO createDto);
    Task<Result> UpdateHotelAsync(int id, UpdateHotelDTO updateDto);
    Task<Result> DeleteHotelAsync(int id);
}