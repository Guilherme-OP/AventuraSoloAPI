using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class IdiomasController : ControllerBase
{
    private readonly AppDbContext _context;

    public IdiomasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("aventuras")]
    public async Task<ActionResult<IEnumerable<Idioma>>> GetIdiomasAventuras ()
    {
        try
        {
            var idiomas = await _context.Idiomas.Include(i => i.Aventuras).AsNoTracking().ToListAsync();

            if (idiomas is null)
            {
                return NotFound("Nenhum idioma encontrado");
            }
            return Ok(idiomas);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Idioma>>> Get()
    {
        try
        {
            var idiomas = await _context.Idiomas.AsNoTracking().ToListAsync();
            if (idiomas is null)
            {
                return NotFound("Idioma não encontrado.");
            }
            return Ok(idiomas);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        } 
    }

    [HttpGet("{id:int}", Name="ObterIdioma")]
    public async Task<ActionResult<Idioma>> Get(int id) 
    {
        try
        {
            var idioma = await _context.Idiomas.FirstOrDefaultAsync(i => i.IdiomaId == id);
            if (idioma == null)
            {
                return NotFound($"Idioma não encontrado. Id = {id}.");
            }
            return Ok(idioma);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(Idioma idioma)
    {
        try
        {
            if (idioma == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _context.Idiomas.Add(idioma);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterIdioma", new { id = idioma.IdiomaId }, idioma);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, Idioma idioma)
    {
        try
        {
            if (id != idioma.IdiomaId)
            {
                return NotFound($"Idioma não encontrado. Id = {id}.");
            }

            _context.Entry(idioma).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(idioma);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            // Tenta localizar o Id diretamente no banco de dados
            //var idioma = _context.Idiomas.FirstOrDefault(i => i.IdiomaId == id);

            // Tenta localizar o Id primeiro na memória. É necessário utilizar a chave primária na busca.
            var idioma = _context.Idiomas.Find(id);

            if (idioma == null)
            {
                return BadRequest($"Idioma não encontrado. Id = {id}.");
            }

            _context.Idiomas.Remove(idioma);
            await _context.SaveChangesAsync();

            return Ok(idioma);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
