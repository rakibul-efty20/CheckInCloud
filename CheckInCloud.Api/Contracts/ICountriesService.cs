using CheckInCloud.Api.DTOs.Country;

namespace CheckInCloud.Api.Contracts;

public interface ICountriesService
{
    Task<IEnumerable<GetCountriesDTO>> GetCountriesAsync();
    Task<GetCountryDTO> GetCountryAsync(int id);
    Task<GetCountryDTO> CreateCountryAsync(CreateCountryDTO createCountryDto);
    Task UpdateCountryAsync(int id, UpdateCountryDTO updateCountryDto);
    Task DeleteCountryAsync(int id);
    Task<bool> CountryExistsAsync(int id);
    Task<bool> CountryExistsAsync(string name);
}