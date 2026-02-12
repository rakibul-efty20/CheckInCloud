using CheckInCloud.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using CheckInCloud.Api.DTOs.Country;

namespace CheckInCloud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;


        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountriesDTO>>> Getcountries()
        {
            var countries = await _countriesService.GetCountriesAsync();
          
            return Ok(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDTO>> GetCountry(int id)
        {
            var country = await _countriesService.GetCountryAsync(id);

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
            //update the country
            await _countriesService.UpdateCountryAsync(id, updateCountryDto);

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GetCountryDTO>> PostCountry(CreateCountryDTO createCountryDto)
        {
            //create the country

            var resultDto = await _countriesService.CreateCountryAsync(createCountryDto);

            return CreatedAtAction("GetCountry", new { id = resultDto.CountryId },resultDto);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //delete the country
            await _countriesService.DeleteCountryAsync(id);

            return NoContent();
        }
       
    }
}
