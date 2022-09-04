using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Api.Application.Interfaces.Repositories;
using KatilimciSozluk.Common.Infrastructure.Exceptions;
using KatilimciSozluk.Common.Models.RequestModels;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.User.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.FirstOrDefaultAsync(u => u.Id == request.Id);
            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            await _userRepository.DeleteAsync(dbUser);
            return true;

        }
    }
}
