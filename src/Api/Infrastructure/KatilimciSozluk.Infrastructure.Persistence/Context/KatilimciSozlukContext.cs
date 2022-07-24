using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.Context;

public class KatilimciSozlukContext : DbContext
{
    public const string DEFAULT_SCHEMA = "dbo";
    private readonly IConfiguration _configuration;

    public KatilimciSozlukContext()
    {

    }

    public KatilimciSozlukContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Entry> Entries { get; set; } = null!;

    public DbSet<EntryVote> EntryVotes { get; set; } = null!;
    public DbSet<EntryFavorite> EntryFavorites { get; set; } = null!;

    public DbSet<EntryComment> EntryComments { get; set; } = null!;
    public DbSet<EntryCommentVote> EntryCommentVotes { get; set; } = null!;
    public DbSet<EntryCommentFavorite> EntryCommentFavorites { get; set; } = null!;

    public DbSet<EmailConfirmation> EmailConfirmations { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
             
            var connStr = _configuration.GetConnectionString("KatilimciSozlukDb");
            optionsBuilder.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        OnBeforeSave();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSave();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSave();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSave()
    {
        var addedEntites = ChangeTracker.Entries()
                                .Where(i => i.State == EntityState.Added)
                                .Select(i => (BaseEntity)i.Entity);

        PrepareAddedEntities(addedEntites);
    }

    private void PrepareAddedEntities(IEnumerable<BaseEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.CreateDate == DateTime.MinValue)
                entity.CreateDate = DateTime.Now;
        }
    }
}
