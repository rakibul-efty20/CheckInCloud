using System.ComponentModel.DataAnnotations;

namespace CheckInCloud.Api.DTOs.Country;

public class UpdateCountryDTO : CreateCountryDTO
{
    [Required] 
    public int CountryId { get; set; }
}