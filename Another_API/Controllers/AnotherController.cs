using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Another_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnotherController : ControllerBase
    {
        [HttpGet("anotherendpoint")]
        public string Get()
        {
            return "If you see this, you're auth :D";
        }
    }
}
