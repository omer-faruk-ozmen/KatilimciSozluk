﻿using AutoMapper;
using KatilimciSozluk.Api.Application.Interfaces.Repositories;
using KatilimciSozluk.Common.Models.Queries;
using KatilimciSozluk.Common.ViewModels.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KatilimciSozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private readonly IEntryRepository entryRepository;

    public GetEntryDetailQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        this.entryRepository = entryRepository;
    }

    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        var query = entryRepository.AsQueryable();

        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryVotes)
            .Where(i => i.Id == request.EntryId);

        var list = query.Select(i => new GetEntryDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = i.EntryFavorites.Count,
            CreatedDate = i.CreateDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType =
                request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                    ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                    : VoteType.None
        });

        return await list.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}