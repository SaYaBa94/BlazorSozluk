using BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryDetails
{
    public class GetEntryDetailQuery: IRequest<GetEntryDetailViewModel>
    {
        public GetEntryDetailQuery(Guid entryId, Guid? userId)
        {
            EntryId = entryId;
            UserId = userId;
        }

        public Guid EntryId { get; set; }
        public Guid? UserId { get; set; }
    }
}
