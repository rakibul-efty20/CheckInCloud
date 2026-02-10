using Microsoft.Build.Framework;

namespace CheckInCloud.Api.Data;

public class Country
{
    public int CountryId { get; set; }

    public  string Name { get; set; }

    public required string ShortName { get; set; }

    public IList<Hotel> Hotels { get; set; } = [];
}