using CheckInCloud.Api.Contracts;
using Microsoft.AspNetCore.Mvc;
using CheckInCloud.Api.DTOs.Country;
using Microsoft.AspNetCore.Authorization;

namespace CheckInCloud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountriesController(ICountriesService countriesService) : BaseApiController
    {
        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountriesDTO>>> Getcountries()
        {
            var countries = await countriesService.GetCountriesAsync();

            return ToActionResult(countries);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDTO>> GetCountry(int id)
        {
            var country = await countriesService.GetCountryAsync(id);

            return ToActionResult(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDTO  updateCountryDto)
        {
            //update the country
           var result = await countriesService.UpdateCountryAsync(id, updateCountryDto);

            return ToActionResult(result);
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<GetCountryDTO>> PostCountry(CreateCountryDTO createCountryDto)
        {
            //create the country

            var result = await countriesService.CreateCountryAsync(createCountryDto);

            if(!result.IsSuccess) return MapErrorsToResponse(result.Errors);

            return CreatedAtAction(nameof(GetCountry), new { id = result.Value!.CountryId },result.Value);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //delete the country
            var result =await countriesService.DeleteCountryAsync(id);

            return ToActionResult(result);
        }
       
    }
}
//administrator
//admin12@gmail.com
//Admin@123

//user
//user12@gmail.com
//User@123