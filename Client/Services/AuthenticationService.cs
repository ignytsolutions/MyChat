using MyChat.Shared.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MyChat.Client.Services {
    public class AuthenticationService : IAuthenticationService {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<CurrentUser> CurrentUserInfo() {
            var result = await _httpClient.GetFromJsonAsync<CurrentUser>("api/authentication/currentuserinfo");

            return result;
        }

        public async Task Login(LoginRequest loginRequest) {
            var result = await _httpClient.PostAsJsonAsync("api/authentication/login", loginRequest);

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) 
                throw new Exception(await result.Content.ReadAsStringAsync());

            result.EnsureSuccessStatusCode();
        }

        public async Task Logout() {
            var result = await _httpClient.PostAsync("api/authentication/logout", null);

            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterRequest registerRequest) {
            var result = await _httpClient.PostAsJsonAsync("api/authentication/register", registerRequest);

            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) 
                throw new Exception(await result.Content.ReadAsStringAsync());

            result.EnsureSuccessStatusCode();
        }
    }
}
