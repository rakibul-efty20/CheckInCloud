using CheckInCloud.Api.DTOs.Hotel;

namespace CheckInCloud.Api.Contracts;

public interface IHotelsService
{
    Task<IEnumerable<GetHotelDTO>> GetHotelsAsync();
    Task<GetHotelDTO> GetHotelAsync(int id);
    Task<GetHotelDTO> CreateHotelAsync(CreateHotelDTO createHotelDto);
    Task UpdateHotelAsync(int id, UpdateHotelDTO updateHotelDto);
    Task DeleteHotelAsync(int id);
    Task<bool> HotelExistsAsync(int id);
    Task<bool> HotelExistsAsync(string name);
}