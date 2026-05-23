using Microsoft.EntityFrameworkCore;
using VoteMaster.Models;

namespace VoteMaster.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users;
    public DbSet<RefreshToken> RefreshTokens;
    public DbSet<MainVoting> Votings;
}