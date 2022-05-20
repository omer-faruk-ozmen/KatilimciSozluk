using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.Entry;
using KatilimciSozluk.Common.Infrastructure;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.Entry.DeleteFav
{
    internal class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:KatilimciSozlukConstants.FavExchangeName,
                exchangeType:KatilimciSozlukConstants.DefaultExchangeType,
                queueName:KatilimciSozlukConstants.DeleteEntryFavQueueName,
                obj:new DeleteEntryFavEvent()
                {
                    EntryId = request.EntryId,
                    CreatedBy = request.UserId
                });
            return await Task.FromResult(true);
        }
    }
}
