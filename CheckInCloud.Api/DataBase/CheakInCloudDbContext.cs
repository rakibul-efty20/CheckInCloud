using CheckInCloud.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CheckInCloud.Api.DataBase
{
    public class CheakInCloudDbContext(DbContextOptions<CheakInCloudDbContext> options) : DbContext(options)
    {
       public DbSet<Country> Countries { get; set; }
       public DbSet<Hotel> Hotels { get; set; }
    }
}
