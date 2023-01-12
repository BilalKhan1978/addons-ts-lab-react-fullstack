using addons_ts_lab_puppiesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace addons_ts_lab_puppiesApi.Data
{
    public class PuppyDbContext : DbContext
    {
        public PuppyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Puppy> Puppies { get; set; } 
    }
}