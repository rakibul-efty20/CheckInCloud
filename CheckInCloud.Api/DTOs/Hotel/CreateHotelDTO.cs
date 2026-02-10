using System.ComponentModel.DataAnnotations;

namespace CheckInCloud.Api.DTOs.Hotel
{
    public class CreateHotelDTO
    {

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public int CountryId { get; set; }
    }
}
