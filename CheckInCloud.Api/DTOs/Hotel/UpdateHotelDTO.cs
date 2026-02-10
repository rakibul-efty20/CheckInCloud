using System.ComponentModel.DataAnnotations;

namespace CheckInCloud.Api.DTOs.Hotel;

public class UpdateHotelDTO : CreateHotelDTO
{
    [Required]
    public int Id { get; set; }
}