using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using KatilimciSozluk.Common.Events.Entry;
using Microsoft.Data.SqlClient;

namespace KatilimciSozluk.Projections.FavoriteService.Services
{
    public class FavService
    {
        private readonly string connectionString;

        public FavService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task CreateEntryFav(CreateEntryFavEvent @event)
        {
            using var connection = new SqlConnection(connectionString);

            await connection
                .ExecuteAsync(
                "INSERT INTO EntryFavorite (Id, EntryId, CreatedById, CreateDate) VALUES(@Id, @EntryId, @CreatedById, GETDATE())",
                new
                {
                    Id = Guid.NewGuid(),
                    EntryId = @event.EntryId,
                    CreatedById = @event.CreatedBy,
                });
        }
    }
}
