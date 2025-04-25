using Microsoft.EntityFrameworkCore;

namespace ms_cards.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tarjeta> Tarjetas => Set<Tarjeta>();
}