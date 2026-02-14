using CheckInCloud.Api.Contracts;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DTOs.Hotel;
using CheckInCloud.Api.Services;
using HotelListing.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheckInCloud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : BaseApiController
    {
        private readonly IHotelsService _hotelsService;


        public HotelsController(IHotelsService hotelsService)
        {
            _hotelsService = hotelsService;
        }
          

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDTO>>> GetHotels()
        {
            var hotels = await _hotelsService.GetHotelsAsync();
        
            return ToActionResult(hotels);
        } 

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDTO>> GetHotel(int id)
        {
            var hotel = await _hotelsService.GetHotelAsync(id);

           
            return ToActionResult(hotel);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDTO updateHotelDto)
        {
            if (id != updateHotelDto.Id)
            {
                return BadRequest("Id route value must match payload Id.");
            }

            var result = await _hotelsService.UpdateHotelAsync(id, updateHotelDto);
            return ToActionResult(result);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(CreateHotelDTO createHotelDto)
        {
           var hotel = await _hotelsService.CreateHotelAsync(createHotelDto);
           if (!hotel.IsSuccess) return MapErrorsToResponse(hotel.Errors);

           return CreatedAtAction(nameof(GetHotel), new { id = hotel.Value!.Id }, hotel.Value); 
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var result = await _hotelsService.DeleteHotelAsync(id);
            return ToActionResult(result);
        }
    }
}
