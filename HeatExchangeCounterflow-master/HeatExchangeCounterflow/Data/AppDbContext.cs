using HeatExchangeCounterflow.Models;
using Microsoft.EntityFrameworkCore;

namespace HeatExchangeCounterflow.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Calculation> Calculations { get; set; }
    public DbSet<LayerPoint> LayerPoints { get; set; }
}
