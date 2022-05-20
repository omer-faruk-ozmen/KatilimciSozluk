using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommand:IRequest<bool>   
    {
        public DeleteEntryFavCommand(Guid userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }

        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }


    }
}
