using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteVote;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteVote;
using BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
using BlazorSozluk.Api.Application.Features.Queries.GetUserDetail;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : BaseController
    {
        private readonly IMediator _mediator;

        public VoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(id));

            return Ok(user);
        }
        [HttpGet]
        [Route("UserName/{userName}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(Guid.Empty, userName));

            return Ok(user);
        }


        [HttpPost]
        [Route("Entry/{entryId}")]
        public async Task<IActionResult> CreateEntryVote(Guid entryId, VoteType voteType=VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryVoteCommand(entryId,voteType, UserId.Value));

            return Ok(result);
        }    
        
        [HttpPost]
        [Route("EntryComment/{entryCommentId}")]
        public async Task<IActionResult> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType=VoteType.UpVote)
        {
            var result = await _mediator.Send(new CreateEntryCommentVoteCommand(entryCommentId,voteType, UserId.Value));

            return Ok(result);
        }
        [HttpPost]
        [Route("DeleteEntryVote/{entryId}")]
        public async Task<IActionResult> DeleteEntryVote(Guid entryId)
        {
            var result = await _mediator.Send(new DeleteEntryVoteCommand(entryId, UserId.Value));

            return Ok(result);
        } 
        [HttpPost]
        [Route("DeleteEntryCommentVote/{entryCommentId}")]
        public async Task<IActionResult> DeleteEntryCommentVote(Guid entryCommentId)
        {
            var result = await _mediator.Send(new DeleteEntryCommentVoteCommand(entryCommentId, UserId.Value));

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var guid = await _mediator.Send(command);

            return Ok(guid);
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var guid = await _mediator.Send(command);

            return Ok(guid);
        }

        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var guid = await _mediator.Send(new ConfirmEmailCommand(id));

            return Ok(guid);
        }
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;

            var guid = await _mediator.Send(command);

            return Ok(guid);
        }
    }
}
