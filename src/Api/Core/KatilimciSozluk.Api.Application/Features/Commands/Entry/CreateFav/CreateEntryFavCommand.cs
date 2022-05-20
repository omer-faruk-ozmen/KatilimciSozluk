using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommand :IRequest<bool>
    {
        public CreateEntryFavCommand(Guid? userId, Guid? entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }

        public Guid? EntryId { get; set; }
        public Guid? UserId { get; set; }
        
        
    }
}
