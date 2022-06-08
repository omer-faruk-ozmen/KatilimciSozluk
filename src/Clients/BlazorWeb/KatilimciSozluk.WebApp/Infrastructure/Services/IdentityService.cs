using Blazored.LocalStorage;
using KatilimciSozluk.Common.Infrastructure.Exceptions;
using KatilimciSozluk.Common.Infrastructure.Results;
using KatilimciSozluk.Common.Models.Queries;
using KatilimciSozluk.Common.Models.RequestModels;
using KatilimciSozluk.WebApp.Infrastructure.Extensions;
using KatilimciSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;
using KatilimciSozluk.Common.ViewModels.Queries;
using KatilimciSozluk.Common.ViewModels.RequestModels;
using KatilimciSozluk.WebApp.Infrastructure.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace KatilimciSozluk.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ISyncLocalStorageService _syncLocalStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;


        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            this._httpClient = httpClient;
            this._syncLocalStorageService = syncLocalStorageService;
            this._authenticationStateProvider = authenticationStateProvider;
        }


        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return _syncLocalStorageService.GetToken();
        }

        public string GetUserName()
        {
            return _syncLocalStorageService.GetToken();
        }

        public Guid GetUserId()
        {
            return _syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string? responseStr;
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/User/Login", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation?.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }


            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token)) // login success
            {
                _syncLocalStorageService.SetToken(response.Token);
                _syncLocalStorageService.SetUsername(response.UserName);
                _syncLocalStorageService.SetUserId(response.Id);

                //TODO Check after auth
                ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogin(response.UserName, response.Id);

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.UserName);

                return true;
            }

            return false;
        }

        public void Logout()
        {
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);

            // TODO Check after auth
            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
