using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryDetails
{
    public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
    {
        private readonly IEntryRepository _entryRepository;

        public GetEntryDetailQueryHandler(IEntryRepository entryRepository, IMapper mapper)
        {
            _entryRepository = entryRepository;
        }
        public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
        {
            var query = _entryRepository.AsQueryAble();

            query = query.Include(x => x.EntryFavorites)
                .Include(x => x.CreatedBy)
                .Include(x => x.EntryVotes)
                .Where(x=>x.Id==request.EntryId);

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

            return await list.FirstOrDefaultAsync(cancellationToken);


        }
    }
}
