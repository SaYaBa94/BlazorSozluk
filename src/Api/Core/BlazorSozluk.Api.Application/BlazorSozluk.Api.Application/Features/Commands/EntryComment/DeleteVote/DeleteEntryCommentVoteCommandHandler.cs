using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.EntryComment;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote
{

    public class DeleteEntryCommentVoteCommand : IRequest<bool>
    {
        public DeleteEntryCommentVoteCommand(Guid entryCommentId, VoteType voteType, Guid userId)
        {
            entryCommentId = EntryCommentId;
            UserId = userId;
        }

        public Guid EntryCommentId { get; set; }
        public Guid UserId { get; set; }
    }

    public class DeleteEntryCommentVoteCommandHandler : IRequestHandler<DeleteEntryCommentVoteCommand, bool>
    {
        private readonly IEntryCommentRepository _EntryCommentRepository;
        private readonly IMapper _mapper;

        public DeleteEntryCommentVoteCommandHandler(IEntryCommentRepository EntryCommentRepository, IMapper mapper)
        {
            _EntryCommentRepository = EntryCommentRepository;
            _mapper = mapper;
        }

       
        public Task<bool> Handle(DeleteEntryCommentVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:BlazorSozlukConstants.VoteExchangeName,
                exchangeType:BlazorSozlukConstants.DefaultExchangeType,
                queueName:BlazorSozlukConstants.DeleteEntryCommentVoteQueueName,
                obj:new DeleteEntryCommentVoteEvent() {
                    EntryCommentId=request.EntryCommentId, 
                    CreatedBy=request.UserId });

            return Task.FromResult(true);
        }
    }
}
