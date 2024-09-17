
using Microsoft.AspNetCore.Mvc;

namespace Microservice.Catalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            return Ok(id);
        }
    }
}