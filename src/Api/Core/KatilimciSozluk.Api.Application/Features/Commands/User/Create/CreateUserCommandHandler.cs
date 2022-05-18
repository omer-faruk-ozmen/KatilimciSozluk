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

namespace KatilimciSozluk.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (existsUser is not null)
                throw new DatabaseValidationException("User already exists");

            var dbUser = mapper.Map<Domain.Models.User>(request);

            var rows = await userRepository.AddAsync(dbUser);

            //Email Changed/Created
            if (rows > 0)
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
            }

            return dbUser.Id;
        }
    }
}
