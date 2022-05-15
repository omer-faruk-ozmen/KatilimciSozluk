using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Api.Infrastructure.Persistence.Context;
using KatilimciSozluk.Api.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.EntityConfiguration;

public class EmailConfirmationEntityConfiguration : BaseEntityConfiguration<EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.ToTable("emailconfirmation", KatilimciSozlukContext.DEFAULT_SCHEMA);
    }
}