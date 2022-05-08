using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatilimciSozluk.Infrastructure.Persistence.EntityConfiguration.Entry;

public class EntryFavoriteEntityConfiguration : BaseEntityConfiguration<EntryFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("entryfavorite", KatilimciSozlukContext.DEFAULT_SCHEMA);


        builder.HasOne(i => i.Entry)
            .WithMany(i => i.EntryFavorites)
            .HasForeignKey(i => i.EntryId);

        builder.HasOne(i => i.CreatedUser)
            .WithMany(i => i.EntryFavorites)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}