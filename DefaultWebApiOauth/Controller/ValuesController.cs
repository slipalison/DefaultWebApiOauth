using System.Web.Http;

namespace DefaultWebApiOauth.Controller
{
    [RoutePrefix("api/v1/Values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        public string Get() => User.Identity.Name;
    }
}