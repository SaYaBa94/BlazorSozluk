using AutoMapper;
using BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasePagedQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        public GetUserEntriesQuery(Guid? userId, string userName=null, int page=1, int pageSize = 10) : base(page, pageSize)
        {
            UserId = userId;
            UserName = userName;
        }

        public Guid? UserId { get; set; }
        public string UserName { get; set; }
       
    }
}
