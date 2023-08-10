using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Components; 

namespace SmartSkus.Core.UI.Utils
{
    public class HitNtryAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
    {
        //private readonly HttpClient _httpClient;
        private readonly AuthenticationDataMemoryStorage _authenticationDataMemoryStorage;

        public string Username { get; set; } = "";

        private readonly IHttpClientFactory _httpClientFactory;

        //[Parameter] public string? Action { get; set; }

        //[Parameter] public int? Id { get; set; }

        public HitNtryAuthenticationStateProvider(HttpClient httpClient, 
            AuthenticationDataMemoryStorage authenticationDataMemoryStorage, IHttpClientFactory httpClientFactory)
        {
           // _httpClient = httpClient;
            _authenticationDataMemoryStorage = authenticationDataMemoryStorage;

            AuthenticationStateChanged += OnAuthenticationStateChanged;

            _httpClientFactory = httpClientFactory;
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            var authenticationState = await task;

            if (authenticationState is not null)
            {
                Username = authenticationState.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "";
            }
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var identity = new ClaimsIdentity();

            if (tokenHandler.CanReadToken(_authenticationDataMemoryStorage.Token))
            {
                var jwtSecurityToken = tokenHandler.ReadJwtToken(_authenticationDataMemoryStorage.Token);
                identity = new ClaimsIdentity(jwtSecurityToken.Claims, "AdminSku");
            }

            var principal = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(principal);
            var authenticationTask = Task.FromResult(authenticationState);

            return authenticationTask;
        }

        public async Task LoginAsync()
        {
            // TODO: Replace hardcoded JWT with API call
            //string token = await _httpClient.GetStringAsync("example-data/token_new.json");

            var client = _httpClientFactory.CreateClient("WebAPI");
            string token = await client.GetStringAsync(client.BaseAddress + "example-data/token_new.json");

            _authenticationDataMemoryStorage.Token = token;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Logout()
        {
            _authenticationDataMemoryStorage.Token = "";
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            

        }

        public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChanged;
    }
}
