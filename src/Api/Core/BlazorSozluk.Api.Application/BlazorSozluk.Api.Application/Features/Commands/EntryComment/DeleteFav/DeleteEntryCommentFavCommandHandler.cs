using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
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

namespace BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav
{
    public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
    {
        private readonly IEntryCommentRepository _EntryCommentRepository;
        private readonly IMapper _mapper;

        public DeleteEntryCommentFavCommandHandler(IEntryCommentRepository EntryCommentRepository, IMapper mapper)
        {
            _EntryCommentRepository = EntryCommentRepository;
            _mapper = mapper;
        }

       
        public Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
        {
            QueueFactory.SendMessageToExchange(exchangeName:BlazorSozlukConstants.FavExchangeName,
                exchangeType:BlazorSozlukConstants.DefaultExchangeType,
                queueName:BlazorSozlukConstants.DeleteEntryCommentFavQueueName,
                obj:new DeleteEntryCommentFavEvent() {
                    EntryCommentId=request.EntryCommentId, 
                    CreatedBy=request.UserId });

            return Task.FromResult(true);
        }
    }
}
