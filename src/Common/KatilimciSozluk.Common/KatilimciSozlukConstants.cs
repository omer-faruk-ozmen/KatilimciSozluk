using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KatilimciSozluk.Common
{
    public class KatilimciSozlukConstants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";

        //UserExchange
        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        //FavExchange
        public const string FavExchangeName = "FavExchange";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue";
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue";
        public const string CreateEntryFavQueueName = "CreateEntryFavQueue";
        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueue";
        //VoteExchange
        public const string VoteExchangeName = "VoteExchange";
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue";
        public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueue";

        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue";
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue";



    }
}
