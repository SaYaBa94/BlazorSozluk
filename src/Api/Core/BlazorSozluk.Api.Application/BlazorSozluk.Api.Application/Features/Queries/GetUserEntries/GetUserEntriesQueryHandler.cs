using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository _entryRepository;

        public GetUserEntriesQueryHandler(IEntryRepository entryRepository, IMapper _mapper)
        {
            _entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryAble();
            query = query.Include(x => x.EntryFavorites)
                       .Include(x => x.CreatedBy);

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
            {
                query = query.Where(x => x.CreatedById == request.UserId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
                query = query.Where(x => x.CreatedBy.UserName == request.UserName);
            else
                return null;



            var list = query.Select(x => new GetUserEntriesDetailViewModel()
            {
                Id = x.Id,
                Subject = x.Subject,
                Content = x.Content,
                IsFavorited = request.UserId.HasValue && x.EntryFavorites.Any(y => y.CreatedById == request.UserId),
                FavoritedCount = x.EntryFavorites.Count,
                CreatedDate = x.CreateDate,
                CreatedByUserName = x.CreatedBy.UserName,

            });
            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
