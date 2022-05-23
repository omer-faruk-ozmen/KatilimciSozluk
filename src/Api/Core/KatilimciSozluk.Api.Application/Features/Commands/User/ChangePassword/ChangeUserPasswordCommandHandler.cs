using KatilimciSozluk.Api.Application.Interfaces.Repositories;
using KatilimciSozluk.Common.Events.User;
using KatilimciSozluk.Common.Infrastructure;
using KatilimciSozluk.Common.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatilimciSozluk.Api.Application.Features.Commands.User.ChangePassword
{
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
    {
        private readonly IUserRepository userRepository;

        public ChangeUserPasswordCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (!request.UserId.HasValue)
            {
                throw new ArgumentNullException(nameof(request.UserId));
            }
            var dbUser = await userRepository.GetByIdAsync(request.UserId.Value);

            if (dbUser is null)
                throw new DatabaseValidationException("User not found!");

            var encPass = PasswordEncryptor.Encrypt(request.OldPassword);


            if (dbUser.Password != encPass)
                throw new DatabaseValidationException("Old password wrong!");

            dbUser.Password = PasswordEncryptor.Encrypt(request.NewPassword);


            await userRepository.UpdateAsync(dbUser);
            return true;
        }
    }
}
