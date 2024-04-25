using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BlazorSozluk.Common.Infrastructure.Extensions;

namespace BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries
{
    public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
        }
        public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryAble();
            query = query.Include(x => x.EntryFavorites)
                .Include(x => x.CreatedBy)
                .Include(x => x.EntryVotes);

            var list = query.Select(x => new GetEntryDetailViewModel()
            {
                Id = x.Id,
                Subject = x.Subject,
                Content = x.Content,
                IsFavorited = request.UserId.HasValue && x.EntryFavorites.Any(y => y.CreatedById == request.UserId),
                FavoritedCount = x.EntryFavorites.Count,
                CreatedDate = x.CreateDate,
                CreatedByUserName = x.CreatedBy.UserName,
                VoteType = request.UserId.HasValue && x.EntryVotes.Any(y => y.CreatedById == request.UserId)
                ? x.EntryVotes.FirstOrDefault(y => y.CreatedById == request.UserId).VoteType
                : VoteType.None
            });
            var entries = await list.GetPaged(request.Page, request.PageSize);

            return new PagedViewModel<GetEntryDetailViewModel>(entries.Results, entries.PageInfo);


        }
    }
}
