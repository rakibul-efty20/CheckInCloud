using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckInCloud.Api.Constants;
using CheckInCloud.Api.Contracts;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DataBase;
using CheckInCloud.Api.DTOs.Hotel;
using CheckInCloud.Api.Results;
using HotelListing.Api.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CheckInCloud.Api.Services
{
    public class HotelsService(CheakInCloudDbContext _context,
        ICountriesService countriesService,
        IMapper mapper) : IHotelsService
    {
        public async Task<Result<IEnumerable<GetHotelDTO>>> GetHotelsAsync()
        {
            //Select * FROM Hotels LEFT JOIN Countries ON Hotels.CountryId = Countries.Id
            var hotels = await _context.Hotels
                .Include(q => q.Country)
                .Select(h => new GetHotelDTO(h.Id, h.Name, h.Address, h.Rating, h.CountryId,h.Country!.Name))
                .ToListAsync();


            return Result<IEnumerable<GetHotelDTO>>.Success(hotels);
        }

        public async Task<Result<GetHotelDTO>> GetHotelAsync(int id)
        {
            var hotel = await _context.Hotels
                .Where(h => h.Id == id)
                .Include(q =>q.Country)
                .ProjectTo<GetHotelDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (hotel is null)
            {
                return Result<GetHotelDTO>.Failure(new Error(ErrorCodes.NotFound, $"Hotel '{id}' was not found."));
            }

            return Result<GetHotelDTO>.Success(hotel);
        }

        public async Task<Result<GetHotelDTO>> CreateHotelAsync(CreateHotelDTO createHotelDto)
        {
            //var hotel = new Hotel()
            //{
            //    Name = createHotelDto.Name,
            //    Address = createHotelDto.Address,
            //    Rating = createHotelDto.Rating,
            //    CountryId = createHotelDto.CountryId
            //};

            var countryExists = await countriesService.CountryExistsAsync(createHotelDto.CountryId);
            if (!countryExists)
            {
                return Result<GetHotelDTO>.Failure(new Error(ErrorCodes.NotFound, $"Country '{createHotelDto.CountryId}' was not found."));
            }
            var duplicateHotel = await HotelExistsAsync(createHotelDto.Name,createHotelDto.CountryId);
            if (duplicateHotel)
            {
                return Result<GetHotelDTO>.Failure(new Error(ErrorCodes.Conflict, $"Hotel with name '{createHotelDto.Name}' already exists in the specified country."));
            }

            var hotel = mapper.Map<Hotel>(createHotelDto);
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            var resultDto = new GetHotelDTO(
                hotel.Id,
                hotel.Name,
                hotel.Address,
                hotel.Rating,
                hotel.CountryId,
                string.Empty
            );

            return Result<GetHotelDTO>.Success(resultDto);
        }

        public async Task<Result> UpdateHotelAsync(int id, UpdateHotelDTO updateHotelDto)
        {
            if (id != updateHotelDto.Id)
            {
                return Result.BadRequest(new Error(ErrorCodes.Validation, "Id route value does not match payload Id."));
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel is null)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Hotel '{id}' was not found."));
            }
            var countryExists = await countriesService.CountryExistsAsync(updateHotelDto.CountryId);
            if (!countryExists)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Country '{updateHotelDto.CountryId}' was not found."));
            }



            mapper.Map(updateHotelDto, hotel);

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteHotelAsync(int id)
        {
            var affected = await _context.Hotels
                .Where(q => q.Id == id)
                .ExecuteDeleteAsync();

            if (affected == 0)
            {
                return Result.NotFound(new Error(ErrorCodes.NotFound, $"Hotel '{id}' was not found."));
            }

            return Result.Success();

        }

        public async Task<bool> HotelExistsAsync(int id)
        {
            return await _context.Hotels.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> HotelExistsAsync(string name, int countryId)
        {
            return await _context.Hotels
                .AnyAsync(e => e.Name.ToLower().Trim() == name.ToLower().Trim() && e.CountryId == countryId);
        }
    }
}
