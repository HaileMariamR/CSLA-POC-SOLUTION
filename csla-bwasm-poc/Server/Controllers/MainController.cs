using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cslabwasmpoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MainController : Csla.Server.Hosts.HttpPortalController
    {
        public MainController(Csla.ApplicationContext applicationContext)
          : base(applicationContext) { }

        [HttpGet]
        public string Get()
        {
            return "Test Running";
        }

        public override Task PostAsync([FromQuery] string operation)
        {
            var result = base.PostAsync(operation);
            return result;
        }
    }
}
