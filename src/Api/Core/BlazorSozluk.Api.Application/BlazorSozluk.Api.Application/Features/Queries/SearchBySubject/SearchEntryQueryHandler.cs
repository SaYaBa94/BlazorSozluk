using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.SearchBySubject
{
    public class SearchEntryQueryHandler : IRequestHandler<SearchEntryQuery, List<SearchEntryViewModel>>
    {
        public readonly IEntryRepository _entryRepository;

        public SearchEntryQueryHandler(IEntryRepository entryRepository)
        {
            _entryRepository = entryRepository;
        }

        public async Task<List<SearchEntryViewModel>> Handle(SearchEntryQuery request, CancellationToken cancellationToken)
        {
            var result = _entryRepository.Get(x => EF.Functions.Like(x.Subject, $"{request.SearchText}%"))
                 .Select(x => new SearchEntryViewModel()
                 {
                     Id = x.Id,
                     Subject=x.Subject,
                 });

            return await result.ToListAsync(cancellationToken);
        }
    }
}
