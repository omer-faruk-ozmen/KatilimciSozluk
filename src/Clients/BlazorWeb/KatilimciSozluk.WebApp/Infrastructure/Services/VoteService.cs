﻿using KatilimciSozluk.Common.ViewModels.Enums;
using KatilimciSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace KatilimciSozluk.WebApp.Infrastructure.Services
{
    public class VoteService : IVoteService
    {
        private readonly HttpClient client;

        public VoteService(HttpClient client)
        {
            this.client = client;
        }

        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await client.PostAsync($"/api/Vote/DeleteEntryVote/{entryId}", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryVote error");
        }

        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            var response = await client.PostAsync($"/api/Vote/DeleteEntryCommentVote/{entryCommentId}", null);
            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryCommentVote error");
        }

        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }
        public async Task CreateEntryDownVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.DownVote);
        }

        public async Task CreateEntryCommentUpVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
        }
        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
        }

        public async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result =  await client.PostAsync($"/api/vote/entry/{entryId}?voteType={voteType}", null);
            if (!result.IsSuccessStatusCode)
                throw new Exception("CreateEntryVote error");
            return result;
        }

        public async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await client.PostAsync($"/api/vote/entry/{entryCommentId}?voteType={voteType}", null);
            if (!result.IsSuccessStatusCode)
                throw new Exception("CreateEntryCommentVote error");
            return result;
        }
    }
}
