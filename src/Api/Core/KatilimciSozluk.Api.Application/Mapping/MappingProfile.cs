using AutoMapper;
using KatilimciSozluk.Api.Domain.Models;
using KatilimciSozluk.Common.ViewModels.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatilimciSozluk.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>()
                .ReverseMap();

        }
    }
}
