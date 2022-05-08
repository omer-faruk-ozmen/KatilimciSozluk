using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatilimciSozluk.Infrastructure.Persistence.EntityConfiguration.EntryComment;

public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentvote", KatilimciSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.EntryComment)
            .WithMany(i => i.EntryCommentVotes)
            .HasForeignKey(i => i.EntryCommentId);
    }
}