using AutoMapper;
using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Common.Models.RequestModels;
using KatilimciSozluk.Common.ViewModels.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Api.Application.Features.Queries.GetEntries;
using KatilimciSozluk.Common.Models.Queries;

namespace KatilimciSozluk.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();

            CreateMap<CreateEntryCommand, Entry>()
                .ReverseMap();
            
            CreateMap<Entry, GetEntriesViewModel>()
                .ForMember(x=>x.CommentCount,y=>y.MapFrom(z=>z.EntryComments.Count));

            CreateMap<CreateEntryCommentCommand, EntryComment>()
                .ReverseMap();
        }
    }
}
