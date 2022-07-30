using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController(IHttpContextAccessor contextAccessor)
        {
        }
    }
}