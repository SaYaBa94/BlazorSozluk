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

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav
{
    public class DeleteEntryFavCommand : IRequest<bool>
    {
        public DeleteEntryFavCommand(Guid entryId, Guid userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public Guid EntryId { get; set; }
        public Guid UserId { get; set; }
    }

    public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public DeleteEntryFavCommandHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

       
        public Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:BlazorSozlukConstants.FavExchangeName,
                exchangeType:BlazorSozlukConstants.DefaultExchangeType,
                queueName:BlazorSozlukConstants.DeleteEntryFavQueueName,
                obj:new DeleteEntryFavEvent() {
                    EntryId=request.EntryId, 
                    CreatedBy=request.UserId });

            return Task.FromResult(true);
        }
    }
}
