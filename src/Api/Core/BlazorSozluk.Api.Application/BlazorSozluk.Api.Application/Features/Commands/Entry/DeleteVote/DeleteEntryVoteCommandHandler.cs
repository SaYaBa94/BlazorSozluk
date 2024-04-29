using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.Entry;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote
{
    public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public DeleteEntryVoteCommandHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

       
        public Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:BlazorSozlukConstants.VoteExchangeName,
                exchangeType:BlazorSozlukConstants.DefaultExchangeType,
                queueName:BlazorSozlukConstants.DeleteEntryVoteQueueName,
                obj:new DeleteEntryVoteEvent() {
                    EntryId=request.EntryId, 
                    CreatedBy=request.UserId });

            return Task.FromResult(true);
        }
    }
}
