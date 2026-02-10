using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DataBase;
using CheckInCloud.Api.DTOs.Hotel;

namespace CheckInCloud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly CheakInCloudDbContext _context;

        public HotelsController(CheakInCloudDbContext context)
        {
            _context = context;
        }
          

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelsDTO>>> GetHotels()
        {
            //Select * FROM Hotels LEFT JOIN Countries ON Hotels.CountryId = Countries.Id
            var hotels = await _context.Hotels
                .Select(h => new GetHotelsDTO(h.Id, h.Name, h.Address, h.Rating, h.CountryId))
                .ToListAsync();

            return Ok(hotels);
        } 

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDTO>> GetHotel(int id)
        {
            var hotel = await _context.Hotels
                .Where(h => h.Id ==id)
                .Select(h => new GetHotelDTO(
                    h.Id,
                    h.Name,
                    h.Address,
                    h.Rating,
                    h.Country!.Name
                    ))
                .FirstOrDefaultAsync();

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDTO hotelDto)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest();
            }
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            hotel.Name = hotelDto.Name;
            hotel.Address = hotelDto.Address;
            hotel.Rating = hotelDto.Rating;
            hotel.CountryId = hotelDto.CountryId;

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO hotelDto)
        {
            var hotel = new Hotel()
            {
                Name = hotelDto.Name,
                Address = hotelDto.Address,
                Rating = hotelDto.Rating,
                CountryId = hotelDto.CountryId
            };
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await  _context.Hotels.AnyAsync(e => e.Id == id);
        }
    }
}
