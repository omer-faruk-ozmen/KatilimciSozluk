using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommand:IRequest<bool>
    {
        public Guid ConfirmationId { get; set; }

    }
}
