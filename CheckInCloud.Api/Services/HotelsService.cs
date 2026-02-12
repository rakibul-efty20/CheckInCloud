using CheckInCloud.Api.Contracts;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DataBase;
using CheckInCloud.Api.DTOs.Hotel;
using CheckInCloud.Api.DTOs.Hotel;
using Microsoft.EntityFrameworkCore;

namespace CheckInCloud.Api.Services
{
    public class HotelsService(CheakInCloudDbContext _context) : IHotelsService
    {
        public async Task<IEnumerable<GetHotelDTO>> GetHotelsAsync()
        {
            //Select * FROM Hotels LEFT JOIN Countries ON Hotels.CountryId = Countries.Id
            var hotels = await _context.Hotels
                .Include(q => q.Country)
                .Select(h => new GetHotelDTO(h.Id, h.Name, h.Address, h.Rating, h.CountryId,h.Country!.Name))
                .ToListAsync();


            return hotels;
        }

        public async Task<GetHotelDTO> GetHotelAsync(int id)
        {
            var hotel = await _context.Hotels
                .Where(h => h.Id == id)
                .Select(h => new GetHotelDTO(
                    h.Id,
                    h.Name,
                    h.Address,
                    h.Rating,
                    h.CountryId,
                    h.Country!.Name
                ))
                .FirstOrDefaultAsync();

            return hotel ?? null;
        }

        public async Task<GetHotelDTO> CreateHotelAsync(CreateHotelDTO createHotelDto)
        {
            var hotel = new Hotel()
            {
                Name = createHotelDto.Name,
                Address = createHotelDto.Address,
                Rating = createHotelDto.Rating,
                CountryId = createHotelDto.CountryId
            };
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

            return resultDto;
        }

        public async Task UpdateHotelAsync(int id, UpdateHotelDTO updateHotelDto)
        {
            var hotel = await _context.Hotels.FindAsync(id) ?? throw new KeyNotFoundException("Hotel not found");


            hotel.Name = updateHotelDto.Name;
            hotel.Address = updateHotelDto.Address;
            hotel.Rating = updateHotelDto.Rating;
            hotel.CountryId = updateHotelDto.CountryId;

            _context.Entry(hotel).State = EntityState.Modified;

            _context.Hotels.Update(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHotelAsync(int id)
        {
            var Hotel = await _context.Hotels
                .Where(h => h.Id == id)
                .ExecuteDeleteAsync();

        }

        public async Task<bool> HotelExistsAsync(int id)
        {
            return await _context.Hotels.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> HotelExistsAsync(string name)
        {
            return await _context.Hotels.AnyAsync(e => e.Name == name);
        }
    }
}
