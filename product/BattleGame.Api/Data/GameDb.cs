using Microsoft.EntityFrameworkCore;
using BattleGame.Api.Models;

namespace BattleGame.Api.Data;

public class GameDb : DbContext
{
    public GameDb(DbContextOptions<GameDb> opts) : base(opts) { }

    public DbSet<Player> Players  => Set<Player>();
    public DbSet<Asset>  Assets   => Set<Asset>();
    public DbSet<PlayerAsset> PlayerAssets => Set<PlayerAsset>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<PlayerAsset>()
         .HasKey(pa => new { pa.PlayerId, pa.AssetId });
    }
}
