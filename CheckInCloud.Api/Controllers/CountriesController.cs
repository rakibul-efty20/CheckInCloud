using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CheckInCloud.Api.Data;
using CheckInCloud.Api.DataBase;
using CheckInCloud.Api.DTOs.Country;
using CheckInCloud.Api.DTOs.Hotel;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CheckInCloud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly CheakInCloudDbContext _context;

        public CountriesController(CheakInCloudDbContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountriesDTO>>> Getcountries()
        {
            var countries = await _context.Countries
                .Select(c => new GetCountriesDTO(
                    c.CountryId,
                    c.Name,
                    c.ShortName
                    ))
                .ToListAsync();
          
            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDTO>> GetCountry(int id)
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

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO  updateCountryDto)
        {
            if (id != updateCountryDto.CountryId)
            {
                return BadRequest();
            }
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            country.Name = updateCountryDto.Name;
            country.ShortName = updateCountryDto.ShortName;


            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetCountryDTO>> PostCountry(CreateCountryDTO createCountryDtocountry)
        {
            var country = new Country
            {
                Name = createCountryDtocountry.Name,
                ShortName = createCountryDtocountry.ShortName
            };

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            var resultDto = new GetCountryDTO(
                country.CountryId,
                country.Name,
                country.ShortName,
                null
            );

          

            return CreatedAtAction("GetCountry", new { id = country.CountryId },resultDto);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _context.Countries.AnyAsync(e => e.CountryId == id);
        }
    }
}
