using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.Entry;
using KatilimciSozluk.Common.Infrastructure;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:KatilimciSozlukConstants.VoteExchangeName,
                exchangeType:KatilimciSozlukConstants.DefaultExchangeType,
                queueName:KatilimciSozlukConstants.DeleteEntryVoteQueueName,
                obj:new DeleteEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.UserId
                });

            return await Task.FromResult(true);
        }
    }
}
