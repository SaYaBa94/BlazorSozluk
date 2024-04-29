using BlazorSozluk.Common.ViewModels;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommand : IRequest<bool>
    {
        public DeleteEntryVoteCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public DeleteEntryVoteCommand(Guid entryId, VoteType voteType, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }
}
