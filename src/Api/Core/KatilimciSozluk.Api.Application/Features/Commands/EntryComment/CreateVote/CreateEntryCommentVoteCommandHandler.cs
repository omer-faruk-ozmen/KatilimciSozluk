using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.EntryComment;
using KatilimciSozluk.Common.Infrastructure;
using KatilimciSozluk.Common.Models.RequestModels;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:KatilimciSozlukConstants.VoteExchangeName,
                exchangeType:KatilimciSozlukConstants.DefaultExchangeType,
                queueName:KatilimciSozlukConstants.CreateEntryCommentVoteQueueName,
                obj:new CreateEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreatedBy = request.CreatedBy,
                    VoteType = request.VoteType
                });

            return await Task.FromResult(true);
        }
    }
}
