using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;
using BlazorSozluk.Common.ViewModels;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services;

public class VoteService : IVoteService
{
    private readonly HttpClient _httpClient;

    public VoteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task DeleteEntryVote(Guid entryId)
    {
        var response = await _httpClient.PostAsync($"/api/Vote/DeleteEntryVote/{entryId}", null);
        if (response != null && !response.IsSuccessStatusCode)
        {
            throw new Exception("DeleteEntryVote Error");
        }
    }


    public async Task CreateEntryUpVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteType.UpVote);

    }
    public async Task CreateEntryDownVote(Guid entryId)
    {
        await CreateEntryVote(entryId, VoteType.DownVote);
    }
    private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
    {
        var response = await _httpClient.PostAsync($"/api/Vote/Entry/{entryId}?voteType={voteType}", null);
        // TODO check success code
        return response;
    }

    public async Task DeleteEntryCommentVote(Guid entryCommentId)
    {
        var response = await _httpClient.PostAsync($"/api/Vote/DeleteEntryCommentVote/{entryCommentId}", null);
        if (response != null && !response.IsSuccessStatusCode)
        {
            throw new Exception("DeleteEntryCommentVote Error");
        }
    }
    public async Task CreateEntryCommentUpVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);

    }
    public async Task CreateEntryCommentDownVote(Guid entryCommentId)
    {
        await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
    }

    private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
    {
        var response = await _httpClient.PostAsync($"/api/Vote/EntryComment/{entryCommentId}?voteType={voteType}", null);
        // TODO check success code
        return response;
    }
}
