using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Api.Infrastructure.Persistence.Context;
using KatilimciSozluk.Api.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.EntityConfiguration.Entry;

public class EntryVoteEntityConfiguration : BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entryvote", KatilimciSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.Entry)
            .WithMany(i => i.EntryVotes)
            .HasForeignKey(i => i.EntryId);
    }
}