﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Api.Application.Interfaces.Repositories;
using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Api.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.Repositories
{
    public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
    {
        public EmailConfirmationRepository(KatilimciSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
