using Microsoft.EntityFrameworkCore;
using shega_meetups_api.Entities;

namespace shega_meetups_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}