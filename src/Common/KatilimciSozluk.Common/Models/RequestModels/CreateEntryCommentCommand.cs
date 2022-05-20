using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace KatilimciSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommentCommand:IRequest<Guid>
    {
        public CreateEntryCommentCommand()
        {
            
        }
        public CreateEntryCommentCommand(Guid createdById, Guid entryId, string content)
        {
            CreatedById = createdById;
            EntryId = entryId;
            Content = content;
        }

        public Guid? EntryId { get; set; }
        public string Content { get; set; }
        public Guid? CreatedById { get; set; }
    }
}
