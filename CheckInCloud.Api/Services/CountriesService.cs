using CheckInCloud.Api.Contracts;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DataBase;
using CheckInCloud.Api.DTOs.Country;
using CheckInCloud.Api.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace CheckInCloud.Api.Services
{
    public class CountriesService(CheakInCloudDbContext _context) : ICountriesService
    {
        public async Task<IEnumerable<GetCountriesDTO>> GetCountriesAsync()
        {
            var countries = await _context.Countries
                .Select(c => new GetCountriesDTO(
                    c.CountryId,
                    c.Name,
                    c.ShortName
                ))
                .ToListAsync();

            return countries;
        }

        public async Task<GetCountryDTO> GetCountryAsync(int id)
        {
            var country = await _context.Countries
                .Where(q => q.CountryId == id)
                .Select(c => new GetCountryDTO(
                    c.CountryId,
                    c.Name,
                    c.ShortName,
                    c.Hotels.Select(h => new GetHotelSlimDTO(
                        h.Id,
                        h.Name,
                        h.Address,
                        h.Rating
                    )).ToList()
                ))
                .FirstOrDefaultAsync();

            return country ?? null;
        }

        public async Task<GetCountryDTO> CreateCountryAsync(CreateCountryDTO createCountryDto)
        {
            var country = new Country
            {
                Name = createCountryDto.Name,
                ShortName = createCountryDto.ShortName
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            var resultDto = new GetCountryDTO(
                country.CountryId,
                country.Name,
                country.ShortName,
                null
            );

            return resultDto;
        }

        public async Task UpdateCountryAsync(int id, UpdateCountryDTO updateCountryDto)
        {
            var country = await _context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");

            country.Name = updateCountryDto.Name;
            country.ShortName = updateCountryDto.ShortName;

            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCountryAsync(int id)
        {
            var country = await _context.Countries.FindAsync(id) ?? throw new KeyNotFoundException("Country not found");
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> CountryExistsAsync(int id)
        {
            return await _context.Countries.AnyAsync(e => e.CountryId == id);
        }

        public async Task<bool> CountryExistsAsync(string name)
        {
            return await _context.Countries.AnyAsync(e => e.Name == name);
        }
    }
}
