using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateVote
{
    public class CreateEntryCommentCommentVoteCommandHandler : IRequestHandler<CreateEntryCommentVoteCommand, bool>
    {
        public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(
                exchangeName: BlazorSozlukConstants.VoteExchangeName,
                exchangeType: BlazorSozlukConstants.DefaultExchangeType,
                queueName: BlazorSozlukConstants.CreateEntryCommentVoteQueueName,
                obj: new CreateEntryCommentVoteEvent()
                {
                    EntryCommentId = request.EntryCommentId,
                    VoteType=request.VoteType,
                    CreatedBy=request.UserId
                });

            return await Task.FromResult(true);
        }
    }
}
