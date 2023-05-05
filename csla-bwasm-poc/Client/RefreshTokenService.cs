

using Data_Layer.Interface;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
namespace cslabwasmpoc.Client
{
    public class RefreshTokenService
{

        private readonly AuthenticationStateProvider _AuthstateProvider;

        private readonly IClientAuthService _authService;
        private readonly ILocalStorage _localStorage;
        private readonly ILogger<RefreshTokenService> _logger;

        public RefreshTokenService(AuthenticationStateProvider authstateProvider, IClientAuthService authService , ILocalStorage localStorage, ILogger<RefreshTokenService> logger)
        {
            _AuthstateProvider = authstateProvider;
            _authService = authService;
            _localStorage = localStorage;
            _logger = logger;
        }

        public async Task<string> TryRefreshToken()
        {

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = await _localStorage.GetStringAsync("jwtToken");
            _logger.LogInformation($"working on tryre {token}");

            var tokenResult = jwtHandler.ReadJwtToken(token);

            var expUnixTimestamp = tokenResult.Payload.Exp.Value;


            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(expUnixTimestamp));

            var timeUTC = DateTime.UtcNow;
            _logger.LogInformation($"working on try refresh token {(expTime - timeUTC).TotalMinutes}");

            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 4) {
                var to = await _authService.RefreshToken();
                _logger.LogInformation($"woring on to {to}");
                
                return to;
            }

            return string.Empty;
        }
    }
}
