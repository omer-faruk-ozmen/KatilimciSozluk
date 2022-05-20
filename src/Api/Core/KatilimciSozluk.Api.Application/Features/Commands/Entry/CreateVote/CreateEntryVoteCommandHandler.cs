using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.Entry;
using KatilimciSozluk.Common.Infrastructure;
using KatilimciSozluk.Common.Models.RequestModels;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.Entry.CreateVote
{
    public class CreateEntryVoteCommandHandler : IRequestHandler<CreateEntryVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:KatilimciSozlukConstants.VoteExchangeName,
                exchangeType:KatilimciSozlukConstants.DefaultExchangeType,
                queueName:KatilimciSozlukConstants.CreateEntryVoteQueueName,
                obj:new CreateEntryVoteEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.CreatedBy,
                    VoteType = request.VoteType
                });

            return await Task.FromResult(true);
        }
    }
}
