using Microsoft.EntityFrameworkCore;

namespace VoteMaster.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<MainVoting> Votings;
}