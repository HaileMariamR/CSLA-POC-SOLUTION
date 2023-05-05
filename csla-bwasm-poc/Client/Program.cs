using csla_bwasm_poc.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Csla.Configuration;
using Csla;
using cslabwasmsecurity;
using Csla.DataPortalClient;
using cslabwasmpoc.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Csla.Blazor.Authentication;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<AuthenticationStateProvider, CslaAuthenticationStateProvider>();  



builder.Services.AddTransient<ILocalStorage, LocalStorage>();

var provider = builder.Services.BuildServiceProvider();
var localStorage = provider.GetRequiredService<ILocalStorage>();
var token = await localStorage.GetStringAsync("jwtToken");


var proxyOptions = new Csla.Channels.Http.HttpProxyOptions();





builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);

builder.Services.AddTransient<IDataPortalProxy>(
   sp =>
   {
       var applicationContext = sp.GetRequiredService<ApplicationContext>();
       var client = applicationContext.GetRequiredService<HttpClient>();
       client.EnableIntercept(sp);
       return new CustomHttpProxy(applicationContext, client, proxyOptions, token);
   });

builder.Services.AddCsla(o => o
  .AddBlazorWebAssembly());

builder.Services.AddScoped<IClientAuthService, ClientAuthService>();
builder.Services.AddScoped<RefreshTokenService>();

builder.Services.AddHttpClientInterceptor();

builder.Services.AddScoped<HttpClientInterceptorService>();




await builder.Build().RunAsync();
 