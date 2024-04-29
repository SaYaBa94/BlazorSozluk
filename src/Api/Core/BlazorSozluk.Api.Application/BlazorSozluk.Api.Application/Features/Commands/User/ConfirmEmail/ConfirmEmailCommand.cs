using MediatR;

namespace BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public ConfirmEmailCommand(Guid confirmationId)
        {
            ConfirmationId = confirmationId;
        }

        public Guid ConfirmationId { get; set; }
    }
}
