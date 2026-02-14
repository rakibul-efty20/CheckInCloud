using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckInCloud.Api.Constants;
using CheckInCloud.Api.Contracts;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DataBase;
using CheckInCloud.Api.DTOs.Country;
using CheckInCloud.Api.DTOs.Hotel;
using CheckInCloud.Api.Results;
using Microsoft.EntityFrameworkCore;

namespace CheckInCloud.Api.Services
{
    public class CountriesService(CheakInCloudDbContext _context,IMapper mapper) : ICountriesService
    {
        public async Task<Result<IEnumerable<GetCountriesDTO>>> GetCountriesAsync()
        {
            var countries = await _context.Countries
                .ProjectTo<GetCountriesDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<IEnumerable<GetCountriesDTO>>.Success(countries);
        }

        public async Task<Result<GetCountryDTO>> GetCountryAsync(int id)
        {
            var country = await _context.Countries
                .Where(q => q.CountryId == id)
                .ProjectTo<GetCountryDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return country is null 
                ? Result<GetCountryDTO>.Failure(new Error(ErrorCodes.NotFound, $"Country with id '{id}' not found"))
                : Result<GetCountryDTO>.Success(country);
        }

        public async Task<Result<GetCountryDTO>> CreateCountryAsync(CreateCountryDTO createCountryDto)
        {
            try
            {
                var exists = await CountryExistsAsync(createCountryDto.Name);
                if (exists)
                {
                    return Result<GetCountryDTO>.Failure(new Error(ErrorCodes.Conflict, $"Country with name '{createCountryDto
                        .Name}' already exists"));
                }

               var country = mapper.Map<Country>(createCountryDto);
                _context.Countries.Add(country);
                await _context.SaveChangesAsync();

                var resultDto = await _context.Countries
                    .Where(q => q.CountryId == country.CountryId)
                    .ProjectTo<GetCountryDTO>(mapper.ConfigurationProvider)
                    .FirstAsync();

                return Result<GetCountryDTO>.Success(resultDto);

            }
            catch (Exception)
            {
                return Result<GetCountryDTO>.Failure(new Error(ErrorCodes.Failure,
                    "An unexpected error occurred while crating the country."));
            }
        }

        public async Task<Result> UpdateCountryAsync(int id, UpdateCountryDTO updateCountryDto)
        {
            try
            {
                if (id != updateCountryDto.CountryId)
                {
                    return Result.BadRequest(new Error(ErrorCodes.Validation, "Id route value does not match payload Id"));
                }

                var country = await _context.Countries.FindAsync(id);
                if(country is null)
                {
                    return Result.NotFound(new Error(ErrorCodes.NotFound, $"Country with id '{id}' not found"));
                }
                //var duplicateName = await CountryExistsAsync(updateCountryDto.Name);
                //if (duplicateName)
                //{
                //    return Result.Failure(new Error(ErrorCodes.Conflict,
                //        $"Country with name'{updateCountryDto.Name}' already exits"));
                //}

                //Use AutoMapper to map the update DTO to the existing country entity
                mapper.Map(updateCountryDto, country);
                await _context.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failure(new Error(ErrorCodes.Failure,"An unexpected error occurred while updating the country."));
            }
        }

        public async Task<Result> DeleteCountryAsync(int id)
        {
            try
            {
                var country = await _context.Countries.FindAsync(id);
                if (country is null)
                {
                    return Result.NotFound(new Error(ErrorCodes.NotFound, $"Country with id '{id}' not found"));
                }

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception)
            {
                return Result.Failure(new Error(ErrorCodes.Failure,"An unexpected error occured while deleting the country"));
            }
        }

        public async Task<bool> CountryExistsAsync(int id)
        {
            return await _context.Countries.AnyAsync(e => e.CountryId == id);
        }

        public async Task<bool> CountryExistsAsync(string name)
        {
            return await _context.Countries
                .AnyAsync(e => e.Name.ToLower().Trim() == name.ToLower().Trim());
        }
    }
}
