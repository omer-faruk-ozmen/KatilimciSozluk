using KatilimciSozluk.Common.ViewModels.Enums;

namespace KatilimciSozluk.WebApp.Infrastructure.Services.Interfaces;

public interface IVoteService
{
    Task DeleteEntryVote(Guid entryId);
    Task DeleteEntryCommentVote(Guid entryCommentId);
    Task CreateEntryUpVote(Guid entryId);
    Task CreateEntryDownVote(Guid entryId);
    Task CreateEntryCommentUpVote(Guid entryCommentId);
    Task CreateEntryCommentDownVote(Guid entryCommentId);
    Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote);
    Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote);
}