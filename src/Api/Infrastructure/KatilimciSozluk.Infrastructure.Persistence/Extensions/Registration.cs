using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Api.Application.Interfaces.Repositories;
using KatilimciSozluk.Api.Infrastructure.Persistence.Context;
using KatilimciSozluk.Api.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<KatilimciSozlukContext>(conf =>
        {
           
            conf.UseSqlServer(@"Server=(localdb)\ProjectsV13;Initial Catalog=KatilimciSozlukDb;Persist Security Info=True;", opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

        return services;
    }
}