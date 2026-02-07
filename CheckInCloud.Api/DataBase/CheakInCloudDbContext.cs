using CheckInCloud.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CheckInCloud.Api.DataBase
{
    public class CheakInCloudDbContext(DbContextOptions<CheakInCloudDbContext> options) : DbContext(options)
    {
        DbSet<Country> countries { get; set; }
        DbSet<Hotel> hotels { get; set; }
    }
}
