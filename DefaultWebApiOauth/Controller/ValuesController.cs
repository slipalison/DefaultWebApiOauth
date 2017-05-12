using System.Web.Http;

namespace DefaultWebApiOauth.Controller
{
    public class ValuesController : ApiController
    {
        [Authorize()]
        public string Get() => User.Identity.Name;
    }
}