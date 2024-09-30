using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_1_Depi.Models;

namespace MVC_1_Depi.Data
{
    public class RunDbContext : IdentityDbContext<AppUser>
    {
        public RunDbContext(DbContextOptions <RunDbContext> options) : base(options)
        {

        }

        public DbSet<Club> Clubs {  get; set; } 
        public DbSet<Race> Races { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
