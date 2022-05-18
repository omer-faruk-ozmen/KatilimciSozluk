using MediatR;

namespace KatilimciSozluk.Api.WebApi.Controllers
{
    internal class ConfirmEmailCommand : IRequest<object>
    {
        public Guid ConfirmationId { get; set; }
    }
}