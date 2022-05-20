using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KatilimciSozluk.Common.ViewModels.Enums;
using MediatR;

namespace KatilimciSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommentVoteCommand:IRequest<bool>
    {
        public CreateEntryCommentVoteCommand()
        {
            
        }
        public CreateEntryCommentVoteCommand(Guid createdBy, VoteType voteType, Guid entryCommentId)
        {
            CreatedBy = createdBy;
            VoteType = voteType;
            EntryCommentId = entryCommentId;
        }


        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid CreatedBy { get; set; }

    }
}
