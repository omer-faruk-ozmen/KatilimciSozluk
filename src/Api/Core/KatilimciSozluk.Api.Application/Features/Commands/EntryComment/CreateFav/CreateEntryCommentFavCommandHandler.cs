using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.EntryComment;
using KatilimciSozluk.Common.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatilimciSozluk.Api.Application.Features.Commands.EntryComment.CreateFav
{
    internal class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName: KatilimciSozlukConstants.FavExchangeName,
                exchangeType: KatilimciSozlukConstants.DefaultExchangeType,
                queueName: KatilimciSozlukConstants.CreateEntryCommentFavQueueName,
                obj: new CreateEntryCommentFavEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreatedBy = request.UserId
                });

            return await Task.FromResult(true);
        }
    }
}
