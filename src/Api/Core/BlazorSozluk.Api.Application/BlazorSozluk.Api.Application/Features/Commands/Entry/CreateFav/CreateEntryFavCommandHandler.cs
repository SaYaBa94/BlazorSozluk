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

namespace BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav
{
    public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
    {
        private readonly IEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public CreateEntryFavCommandHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
            _mapper = mapper;
        }

       
        public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:BlazorSozlukConstants.FavExchangeName,
                exchangeType:BlazorSozlukConstants.DefaultExchangeType,
                queueName:BlazorSozlukConstants.CreateEntryFavQueueName,
                obj:new CreateEntryFavEvent() {
                    EntryId=request.EntryId.Value, 
                    CreatedBy=request.UserId.Value });

            return Task.FromResult(true);
        }
    }
}
