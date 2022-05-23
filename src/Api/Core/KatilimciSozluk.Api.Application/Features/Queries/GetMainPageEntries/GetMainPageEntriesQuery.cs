using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Common.Models.Page;
using KatilimciSozluk.Common.Models.Queries;
using MediatR;

namespace KatilimciSozluk.Api.Application.Features.Queries.GetMainPageEntries
{
    public class GetMainPageEntriesQuery : BasePagedQuery,IRequest<PagedViewModel<GetEntryDetailViewModel>>
    {
        public GetMainPageEntriesQuery(int page, int pageSize, Guid? userId) : base(page, pageSize)
        {
            UserId = userId;
        }

        public Guid? UserId { get; set; }
    }
}
