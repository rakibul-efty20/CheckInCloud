using CheckInCloud.Api.DTOs.Country;
using CheckInCloud.Api.Results;

namespace CheckInCloud.Api.Contracts;

public interface ICountriesService
{
    Task<Result<IEnumerable<GetCountriesDTO>>> GetCountriesAsync();
    Task<Result<GetCountryDTO>> GetCountryAsync(int id);
    Task<Result<GetCountryDTO>> CreateCountryAsync(CreateCountryDTO createCountryDto);
    Task<Result> UpdateCountryAsync(int id, UpdateCountryDTO updateCountryDto);
    Task<Result> DeleteCountryAsync(int id);
    Task<bool> CountryExistsAsync(int id);
    Task<bool> CountryExistsAsync(string name);
}