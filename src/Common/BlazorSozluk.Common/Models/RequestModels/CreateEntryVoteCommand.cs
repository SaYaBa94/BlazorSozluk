using BlazorSozluk.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.RequestModels
{
    public class CreateEntryVoteCommand: IRequest<bool>
    {
        public CreateEntryVoteCommand()
        {
        }

       

        public CreateEntryVoteCommand(Guid entryId, VoteType voteType, Guid userId)
        {
            EntryId = entryId;
            VoteType = voteType;
            UserId = userId;
        }

        public Guid EntryId { get; set; }
        public VoteType VoteType { get; set; }
        public Guid UserId { get; set; }
    }
}
