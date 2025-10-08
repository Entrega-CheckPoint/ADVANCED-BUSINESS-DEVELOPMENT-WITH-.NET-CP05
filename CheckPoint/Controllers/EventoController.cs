using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CheckPoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetProtected()
        {
            return Ok("Bem-vindo! Você está autenticado."); 
        }
    }
}
