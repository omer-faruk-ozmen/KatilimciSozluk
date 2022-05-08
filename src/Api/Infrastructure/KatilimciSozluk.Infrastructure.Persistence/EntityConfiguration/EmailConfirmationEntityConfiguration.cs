using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatilimciSozluk.Infrastructure.Persistence.EntityConfiguration;

public class EmailConfirmationEntityConfiguration : BaseEntityConfiguration<EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.ToTable("emailconfirmation", KatilimciSozlukContext.DEFAULT_SCHEMA);
    }
}