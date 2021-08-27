using HouseRentManagement.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HouseRentManagement.Data
{
    public class DataBaseContext : IdentityDbContext<UserAccountModel>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            
        }
        public DbSet<HouseDetails> HouseDetails { get; set; }
    }
}
