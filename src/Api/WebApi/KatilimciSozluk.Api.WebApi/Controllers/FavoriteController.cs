using KatilimciSozluk.Api.Application.Features.Commands.Entry.CreateFav;
using KatilimciSozluk.Api.Application.Features.Commands.Entry.DeleteFav;
using KatilimciSozluk.Api.Application.Features.Commands.EntryComment.CreateFav;
using KatilimciSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KatilimciSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : BaseController
    {
        private readonly IMediator _mediator;

        public FavoriteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("entry/{entryId}")]
        public async Task<IActionResult> CreateEntryFav(Guid entryId)
        {
            var result = await _mediator.Send(new CreateEntryFavCommand(entryId: entryId, userId: UserId));

            return Ok(result);
        }

        [HttpPost]
        [Route("entrycomment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentFav(Guid entryCommentId)
        {
            var result = await _mediator.Send(new CreateEntryCommentFavCommand(entryCommentId, UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("deleteentryfav/{entryId}")]
        public async Task<IActionResult> DeleteEntryFav(Guid entryId)
        {
            var result = await _mediator.Send(new DeleteEntryFavCommand(entryId: entryId, userId: UserId.Value));

            return Ok(result);
        }

        [HttpPost]
        [Route("deleteentrycommentfav/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentFav(Guid entryCommentId)
        {
            var result = await _mediator.Send(new DeleteEntryCommentFavCommand(entryCommentId, UserId.Value));

            return Ok(result);
        }
    }
}
