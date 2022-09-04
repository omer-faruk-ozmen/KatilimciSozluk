using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace KatilimciSozluk.Common.Models.RequestModels
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }


        public DeleteUserCommand()
        {

        }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
