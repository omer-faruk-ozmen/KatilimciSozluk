using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using KatilimciSozluk.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;

namespace KatilimciSozluk.WebApp.Infrastructure.Auth
{
    public class AuthStateProvider : AuthenticationStateProvider

    {
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationState anonymous;


        public AuthStateProvider(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
            anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var apiToken = await localStorage.GetToken();

            if (string.IsNullOrEmpty(apiToken))
                return anonymous;

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(apiToken);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims,"jwtAuthType"));

            return new AuthenticationState(claimsPrincipal);
        }

        public void NotifyUserLogin(string userName, Guid userId)
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new []
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier,userId.ToString())
            }, "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(claimsPrincipal));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(anonymous);

            NotifyAuthenticationStateChanged(authState);
        }
    }
}
