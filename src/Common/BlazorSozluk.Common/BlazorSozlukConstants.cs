﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common
{
    public class BlazorSozlukConstants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";

        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";

        public const string FavExchangeName = "FavExchange";
        public const string CreateEntryCommentFavQueueName = "CreateEntryCommentFavQueue"; 
        public const string CreateEntryFavQueueName = "CreateEntryFavQueue";   
        public const string DeleteEntryCommentFavQueueName = "DeleteEntryCommentFavQueue"; 
        public const string DeleteEntryFavQueueName = "DeleteEntryFavQueu";  
        
        public const string VoteExchangeName = "VoteExchange";
        public const string CreateEntryCommentVoteQueueName = "CreateEntryCommentVoteQueue"; 
        public const string CreateEntryVoteQueueName = "CreateEntryVoteQueue"; 
        public const string DeleteEntryCommentVoteQueueName = "DeleteEntryCommentVoteQueue"; 
        public const string DeleteEntryVoteQueueName = "DeleteEntryVoteQueu";  

    }
}
