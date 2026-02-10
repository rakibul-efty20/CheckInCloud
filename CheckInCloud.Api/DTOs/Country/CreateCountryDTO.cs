using System.ComponentModel.DataAnnotations;

namespace CheckInCloud.Api.DTOs.Country
{
    public class CreateCountryDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(3)]
        public required string ShortName { get; set; }
    }
}
