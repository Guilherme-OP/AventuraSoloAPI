using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrigemDestinoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrigemDestinoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrigemDestino>>> Get()
        {
            try
            {
                var origemDestino = await _context.OrigensDestinos.AsNoTracking().ToListAsync();
                if (origemDestino is null)
                {
                    return NotFound("Nenhum caminho encontrado.");
                }
                return Ok(origemDestino);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{origemId:int}, {destinoId:int}", Name = "ObterOrigemDestino")]
        public async Task<ActionResult<OrigemDestino>> Get(int origemId, int destinoId)
        {
            try
            {
                var origemDestino = await _context.OrigensDestinos.FirstOrDefaultAsync(od => od.PassoOrigemId == origemId && od.PassoDestinoId == destinoId);

                if (origemDestino == null)
                {
                    return NotFound($"Caminho não encontrado. Origem Id = {origemId} | Destino Id = {destinoId}.");
                }
                return Ok(origemDestino);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(OrigemDestino origemDestino)
        {
            try
            {
                if (origemDestino == null)
                {
                    return BadRequest("Dados inválidos.");
                }

                _context.OrigensDestinos.Add(origemDestino);
                await _context.SaveChangesAsync();

                return new CreatedAtRouteResult("ObterOrigemDestino", new { origemId = origemDestino.PassoOrigemId, destinoId = origemDestino.PassoDestinoId }, origemDestino);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int origemId, int destinoId)
        {
            try
            {
                var origemDestino = await _context.OrigensDestinos.FirstOrDefaultAsync(od => od.PassoOrigemId == origemId && od.PassoDestinoId == destinoId);

                if (origemDestino == null)
                {
                    return BadRequest($"Caminho não encontrado. Origem Id = {origemId} | Destino Id = {destinoId}.");
                }

                _context.OrigensDestinos.Remove(origemDestino);
                _context.SaveChanges();
                return Ok(origemDestino);

                //return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
