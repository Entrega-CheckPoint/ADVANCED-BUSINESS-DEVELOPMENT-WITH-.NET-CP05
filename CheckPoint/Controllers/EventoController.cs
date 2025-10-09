using CheckPoint.Dto;
using CheckPoint.Model;
using CheckPoint.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckPoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        private readonly ServicoEventos _service;

        public EventoController(ServicoEventos service)
        {
            _service = service;
        }

        [HttpGet("login")]
        [Authorize]
        public IActionResult GetProtected()
        {
            return Ok("Bem-vindo! Você está autenticado.");
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<Evento>>> GetEvents() =>
               await _service.GetEventosAsync();

        [HttpGet("{id}", Name = "Evento")]
        [Authorize]
        public async Task<ActionResult<Evento>> GetById(string id)
        {
            var evento = await _service.GetEventoIdAsync(id);
            if (evento == null)
                return NotFound();
            return Ok(evento);
        }

        [HttpGet("paginado")]
        [Authorize]
        public async Task<ActionResult<List<Evento>>> GetHalf([FromQuery] int pagina = 1, [FromQuery] int qtd = 10)
        {

            var limite = Math.Min(qtd, 100);
            var registros = await _service.GetHalfAsync(pagina, limite);
            var total = await _service.CountAsync();

            Response.Headers.Append("X-Total-Count", total.ToString());
            return registros;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Evento>> Create([FromBody] EventoDto evento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var novoEvento = await _service.CreateAsync(evento);
            return CreatedAtRoute("Evento", new { id = novoEvento.Id }, novoEvento);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> Update(string id, Evento evento)
        {
            if (evento == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != evento.Id) return BadRequest();

            var eventoExist = await _service.GetEventoIdAsync(id);
            if (eventoExist == null) return NotFound();

            await _service.UpdateAsync(id, evento);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var eventoExist = await _service.GetEventoIdAsync(id);
            if (eventoExist == null)
                return NotFound();

            var removido = await _service.DeleteAsync(id);
            return removido ? NoContent() : NotFound();
        }

    }
}
