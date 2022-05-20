using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommand:IRequest<bool>
    {
        public DeleteEntryVoteCommand(Guid userId, Guid entryId)
        {
            UserId = userId;
            EntryId = entryId;
        }

        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }

    }
}
