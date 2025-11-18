using EmployesManagementSystemFront.Models;
using System.Net.Http.Json;

namespace EmployesManagementSystemFront.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginModel loginModel);
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(LoginModel loginModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/Login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var token = await response.Content.ReadAsStringAsync();
                    return token.Replace("\"", "");
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
