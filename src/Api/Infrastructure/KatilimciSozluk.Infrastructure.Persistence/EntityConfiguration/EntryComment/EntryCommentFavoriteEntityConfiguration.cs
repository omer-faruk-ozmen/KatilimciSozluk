using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Api.Infrastructure.Persistence.Context;
using KatilimciSozluk.Api.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.EntityConfiguration.EntryComment;

public class EntryCommentFavoriteEntityConfiguration : BaseEntityConfiguration<EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentfavorite", KatilimciSozlukContext.DEFAULT_SCHEMA);


        builder.HasOne(i => i.EntryComment)
            .WithMany(i => i.EntryCommentFavorites)
            .HasForeignKey(i => i.EntryCommentId);

        builder.HasOne(i => i.CreatedUser)
            .WithMany(i => i.EntryCommentFavorites)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}