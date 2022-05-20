using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.EntryComment;
using KatilimciSozluk.Common.Infrastructure;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:KatilimciSozlukConstants.FavExchangeName,
                exchangeType:KatilimciSozlukConstants.DefaultExchangeType,
                queueName:KatilimciSozlukConstants.DeleteEntryCommentFavQueueName,
                obj:new DeleteEntryCommentFavEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    CreatedBy = request.UserId
                });

            return await Task.FromResult(true);
        }
    }
}
