using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Api.Application.Interfaces.Repositories;
using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Api.Infrastructure.Persistence.Context;
using KatilimciSozluk.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace KatilimciSozluk.Api.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(KatilimciSozlukContext dbContext) : base(dbContext)
        {
        }

        public override int Add(User entity)
        {
            entity.Password = PasswordEncryptor.Encrypt(entity.Password);
            return base.Add(entity);
        }

        public override Task<int> AddAsync(User entity)
        {
            entity.Password = PasswordEncryptor.Encrypt(entity.Password);
            return base.AddAsync(entity);
        }

        public override Task BulkAdd(IEnumerable<User>? entities)
        {
            foreach (var user in entities)
            {
                user.Password = PasswordEncryptor.Encrypt(user.Password);
            }
            return base.BulkAdd(entities);
        }

        public override int AddOrUpdate(User entity)
        {
            entity.Password = PasswordEncryptor.Encrypt(entity.Password);
            return base.AddOrUpdate(entity);
        }

        public override Task<int> AddOrUpdateAsync(User entity)
        {
            entity.Password = PasswordEncryptor.Encrypt(entity.Password);
            return base.AddOrUpdateAsync(entity);
        }
    }
}
