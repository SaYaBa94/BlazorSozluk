using BlazorSozluk.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.RequestModels
{
    public class CreateEntryCommentVoteCommand : IRequest<bool>
    {
        public CreateEntryCommentVoteCommand()
        {
        }

        public CreateEntryCommentVoteCommand(Guid entryCommentId, VoteType voteType, Guid userId)
        {
            EntryCommentId = entryCommentId;
            VoteType = voteType;
            UserId = userId;
        }

        public Guid EntryCommentId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid UserId { get; set; }
    }
}
