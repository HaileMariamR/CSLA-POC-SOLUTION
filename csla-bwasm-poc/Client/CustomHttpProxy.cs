using Csla;
using Csla.Channels.Http;
using System.Net.Http.Headers;
using cslabwasmpoc.Client;

namespace cslabwasmsecurity
{



    public class CustomHttpProxy : HttpProxy
    {

           private readonly string _jwtToken;
        public CustomHttpProxy(ApplicationContext applicationContext, HttpClient httpClient, HttpProxyOptions options, string jwtToken) : base(applicationContext, httpClient, options)
        {


            options.DataPortalUrl = "/api/Main";
            _jwtToken = jwtToken;
        }

        protected override  HttpClient GetHttpClient()
        {
            var client = base.GetHttpClient();
             client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken );
            return  client;
        }
  
    }
}
