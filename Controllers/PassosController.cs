using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PassosController : ControllerBase
{
	private readonly AppDbContext _context;

	public PassosController(AppDbContext context)
	{
		_context = context;
	}

    [HttpGet("destinos")]
    public async Task<ActionResult<IEnumerable<Passo>>> GetPassosOrigensDestinos()
    {
        try
        {
            var passos = await _context.Passos.Include(p => p.Origens).AsNoTracking().ToListAsync();

            if (passos is null)
            {
                return NotFound("Nenhum passo encontrado.");
            }
            return Ok(passos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Passo>>> Get()
	{
		try
		{
            var passos = await _context.Passos.AsNoTracking().ToListAsync();
            if (passos is null)
            {
                return NotFound("Nenhum passo encontrado.");
            }
            return Ok(passos);
        }
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpGet("{id:int}", Name = "ObterPasso")]
	public async Task<ActionResult<Passo>> Get(int id) 
	{
		try
		{
            var passo = await _context.Passos.FirstOrDefaultAsync(p => p.PassoId == id);

            if (passo == null)
            {
                return NotFound($"Passo não encontrado. Id = {id}.");
            }

            return Ok(passo);
        }
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpPost]
	public async Task<ActionResult> Post(Passo passo)
	{
		try
		{
            if (passo == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _context.Passos.Add(passo);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterPasso", new { id = passo.PassoId }, passo);
        }
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpPut]
	public async Task<ActionResult> Put(int id, Passo passo) 
	{
		try
		{
            if (id != passo.PassoId)
            {
                return BadRequest($"Dados inválidos. Id = {id}.");
            }

            _context.Entry(passo).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(passo);

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
            var passo = await _context.Passos.FirstOrDefaultAsync(p => p.PassoId == id);

            if (passo == null)
            {
                return BadRequest($"Passo não encontrada. Id = {id}.");
            }

            _context.Passos.Remove(passo);

            _context.SaveChanges();
            return Ok(passo);

            //return NoContent();
        }
        catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}
}
