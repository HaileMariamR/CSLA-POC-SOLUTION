using cslabwasmpoc.Client;
using System.Net.Http.Headers;
using Toolbelt.Blazor;
using System.Diagnostics;
using Microsoft.Extensions.Logging;


namespace cslabwasmpoc.Client
{
    public class HttpClientInterceptorService
    {
    private readonly HttpClientInterceptor _interceptor;
    private readonly RefreshTokenService _refreshTokenService;
    private ILogger<HttpClientInterceptorService> _logger;
    public HttpClientInterceptorService(HttpClientInterceptor interceptor, RefreshTokenService refreshTokenService , ILogger<HttpClientInterceptorService> logger)
        {
            _interceptor = interceptor;
            _refreshTokenService = refreshTokenService;
            _logger = logger;
        }


    public void RegisterEvent() => _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
    public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
    {

   
                var token = await _refreshTokenService.TryRefreshToken();
                
                if (!string.IsNullOrEmpty(token))
                {
                     e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            

        }
    public void DisposeEvent() => _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
}
}
