using BlazorSozluk.Clients.WebApp.Infrastructure.Services.Interfaces;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.Clients.WebApp.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");
            return userDetail;
        }
        public async Task<UserDetailViewModel> GetUserDetail(string userName)
        {
            var userDetail = await _httpClient.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{userName}");
            return userDetail;
        }

        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var res = await _httpClient.PostAsJsonAsync($"/api/user/update", user);
            return res.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
            var response = await _httpClient.PostAsJsonAsync($"/api/User/ChangePassword", command);
            if (response != null && !response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr = await response.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }
                return false;
            }
            return response.IsSuccessStatusCode;

        }
    }
}
