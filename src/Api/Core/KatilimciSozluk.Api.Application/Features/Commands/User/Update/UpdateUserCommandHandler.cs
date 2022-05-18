using AutoMapper;
using KatilimciSozluk.Api.Application.Interfaces.Repositories;
using KatilimciSozluk.Common;
using KatilimciSozluk.Common.Events.User;
using KatilimciSozluk.Common.Infrastructure;
using KatilimciSozluk.Common.Infrastructure.Exceptions;
using KatilimciSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatilimciSozluk.Api.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }
        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetByIdAsync(request.Id);
            

            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress,request.EmailAddress)!=0;

            mapper.Map(request, dbUser);

            var rows = await userRepository.UpdateAsync(dbUser);

            // Check if email changed

            if (emailChanged &&rows > 0)
            {
                var @event = new UserEmailChangeEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress
                };
                QueueFactory.SendMessageToExchange(exchangeName: KatilimciSozlukConstants.UserExchangeName,
                                                    exchangeType: KatilimciSozlukConstants.DefaultExchangeType,
                                                    queueName: KatilimciSozlukConstants.UserEmailChangedQueueName,
                                                    obj: @event);

                dbUser.EmailConfirmed = false;
                await userRepository.UpdateAsync(dbUser);
            }

            return dbUser.Id;

        }
    }
}
