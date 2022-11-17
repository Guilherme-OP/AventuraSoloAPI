using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AventurasController : ControllerBase
{
    private readonly AppDbContext _context;

    public AventurasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("passos")]
    public async Task<ActionResult<IEnumerable<Aventura>>> GetAventurasPassos()
    {
        try
        {
            var aventuras = await _context.Aventuras.Include(p => p.Passos).AsNoTracking().ToListAsync();

            // Exemplo de filtro
            //var aventuras = _context.Aventuras.Include(p => p.Passo).Where(a => a.AventuraId <= 5).ToList();

            if (aventuras is null)
            {
                return NotFound("Nenhuma aventura encontrada.");
            }
            return Ok(aventuras);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Aventura>>> Get()
    {
        try
        {
            var aventuras = await _context.Aventuras.AsNoTracking().ToListAsync();
            if (aventuras is null)
            {
                return NotFound("Nenhuma aventura encontrada.");
            }
            return Ok(aventuras);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:int}", Name = "ObterAventura")]
    public async Task<ActionResult<Aventura>> Get(int id)
    {
        try
        {
            var aventura = await _context.Aventuras.FirstOrDefaultAsync(a => a.AventuraId == id);

            if (aventura == null)
            {
                return NotFound($"Aventura não encontrada. Id = {id}.");
            }
            return Ok(aventura);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }

       
    }

    [HttpPost]
    public async Task<ActionResult> Post(Aventura aventura)
    {
        try
        {
            if (aventura == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _context.Aventuras.Add(aventura);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterAventura", new { id = aventura.AventuraId }, aventura);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut]
    public async Task<ActionResult> Put(int id, Aventura aventura)
    {
        try
        {
            if (id != aventura.AventuraId)
            {
                return NotFound($"Dados inválidos.  Id = {id}.");
            }
            _context.Entry(aventura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(aventura);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        } 
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var aventura = _context.Aventuras.FirstOrDefault(a => a.AventuraId == id);

            if (aventura == null)
            {
                return BadRequest($"Aventura não encontrada. Id = {id}.");
            }

            _context.Aventuras.Remove(aventura);
            await _context.SaveChangesAsync();
            return Ok(aventura);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
