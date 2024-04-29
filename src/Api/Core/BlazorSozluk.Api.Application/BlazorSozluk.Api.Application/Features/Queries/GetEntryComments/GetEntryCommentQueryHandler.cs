using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentQueryHandler : IRequestHandler<GetEntryCommentQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository _entryCommentRepository;

        public GetEntryCommentQueryHandler(IEntryCommentRepository entryCommentRepository)
        {
            _entryCommentRepository = entryCommentRepository;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentQuery request, CancellationToken cancellationToken)
        {
            var query = _entryCommentRepository.AsQueryAble();
            query = query.Include(x => x.EntryCommentFavorites)
                .Include(x => x.CreatedBy)
                .Include(x => x.EntryCommentVotes)
                .Where(x=>x.EntryId==request.EntryId);

            var list = query.Select(x => new GetEntryCommentsViewModel()
            {
                Id = x.Id,
                Content = x.Content,
                IsFavorited = request.UserId.HasValue && x.EntryCommentFavorites.Any(y => y.CreatedById == request.UserId),
                FavoritedCount = x.EntryCommentFavorites.Count,
                CreatedDate = x.CreateDate,
                CreatedByUserName = x.CreatedBy.UserName,
                VoteType = request.UserId.HasValue && x.EntryCommentVotes.Any(y => y.CreatedById == request.UserId)
                ? x.EntryCommentVotes.FirstOrDefault(y => y.CreatedById == request.UserId).VoteType
                : VoteType.None
            });
            var entryComments = await list.GetPaged(request.Page, request.PageSize);

            return entryComments;

        }
    }
}
