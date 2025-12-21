using Microsoft.EntityFrameworkCore;
using HeatExchangeApp.Models;

namespace HeatExchangeApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SavedCalculation> SavedCalculations { get; set; }
    }
}
