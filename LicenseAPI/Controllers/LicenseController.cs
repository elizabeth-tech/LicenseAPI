using LicenseAPI.Models;
using LicenseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LicenseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly LicenseService service;

        public LicenseController(LicenseService service) => this.service = service;

        // POST: /api/license/register
        [HttpPost(template:"register")]
        public IActionResult RegisterKey(RegisterKeyRequest request)
        {
            if (request.Password != "root") return BadRequest();
            service.RegisterKey(request.Key);
            return Ok();
        }

        // GET: /api/license/is-active/mykey
        [HttpGet(template:"is-active/{key}")]
        public bool IsActive(string key) => service.IsActive(key);
    }
}
