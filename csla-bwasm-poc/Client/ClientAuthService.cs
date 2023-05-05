using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Data_Layer.Model;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace cslabwasmpoc.Client
{
    public class ClientAuthService : IClientAuthService
{

        private readonly ILocalStorage _localStorage;
        private readonly ILogger<ClientAuthService> _logger;
        public ClientAuthService(ILocalStorage localStorage, ILogger<ClientAuthService> logger)
        {
            _localStorage = localStorage;
            _logger = logger;
        }
        public async Task<string> RefreshToken()
        {
            var token = await _localStorage.GetStringAsync("jwtToken");
            var refreshToken = await _localStorage.GetStringAsync("refreshToken");
            var userName = await _localStorage.GetStringAsync("userNmae");

            var tokenDto = new RefreshTokenModel { Token = token, RefreshToken = refreshToken, UserName = userName };


            using (HttpClient client = new HttpClient()) {


                var response = await client.PostAsJsonAsync<RefreshTokenModel>("http://localhost:5187/api/Auth/refresh", tokenDto);
                _logger.LogInformation(response.ToString());    

                if (response.IsSuccessStatusCode)
                {
                    LoginResult result = await response.Content.ReadFromJsonAsync<LoginResult>();
                    if (!string.IsNullOrEmpty(result.JwtToken))
                    {

                        await _localStorage.SaveStringAsync("jwtToken", result.JwtToken);
                        await _localStorage.SaveStringAsync("userNmae", result?.UserName);
                        await _localStorage.SaveStringAsync("refreshToken", result?.RefreshToken);
                        _logger.LogInformation($"new token : {result.JwtToken}");

                        return result.JwtToken;
                    }

                }

            }


       

            return string.Empty;


          

            }
    }
}
