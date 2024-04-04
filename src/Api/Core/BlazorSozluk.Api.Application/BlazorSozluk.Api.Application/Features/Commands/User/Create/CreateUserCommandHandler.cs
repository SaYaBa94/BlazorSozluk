using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await _userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);
            if (existsUser is not null)
                throw new DatabaseValidationException("User already exists!");

            var dbUser = _mapper.Map<BlazorSozluk.Api.Domain.Models.User>(request);

            var rows = await _userRepository.AddAsync(dbUser);

            //control

            if (rows>0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = dbUser.EmailAddress,
                };
                QueueFactory.SendMessageToExchange(exchangeName:BlazorSozlukConstants.UserExchangeName,exchangeType: BlazorSozlukConstants.DefaultExchangeType, queueName:BlazorSozlukConstants.UserEmailChangedQueueName,  obj: @event);
            }
            return dbUser.Id;
        }
    }
}
