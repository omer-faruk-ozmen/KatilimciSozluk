using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Api.Infrastructure.Persistence.Context;
using KatilimciSozluk.Infrastructure.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.EntityConfiguration;

public class UserEntityConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("user", KatilimciSozlukContext.DEFAULT_SCHEMA);
    }
}