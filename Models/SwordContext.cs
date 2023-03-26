using Microsoft.EntityFrameworkCore;

namespace SwordAPI.Models
{
    public class SwordContext : DbContext    //Db magic!
    {
        public SwordContext(DbContextOptions<SwordContext> options) : base(options)
        { 
        }

        public DbSet<Swords> Swords { get; set; }    
    }
}
