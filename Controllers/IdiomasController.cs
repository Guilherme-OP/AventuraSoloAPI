using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class IdiomasController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public IdiomasController(IUnitOfWork uof)
    {
        _uow = uof;
    }

    [HttpGet("IdiomaAventuras")]
    public ActionResult<IEnumerable<Idioma>> GetIdiomasAventuras ()
    {
        try
        {
            var idiomas = _uow.IdiomaRepository.GetAventurasPorIdioma().ToList();

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
    public ActionResult<IEnumerable<Idioma>> Get()
    {
        try
        {
            var idiomas = _uow.IdiomaRepository.Get().ToList();
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
    public  ActionResult<Idioma> Get(int id) 
    {
        try
        {
            var idioma = _uow.IdiomaRepository.GetById(i => i.IdiomaId == id);
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
    public  ActionResult Post(Idioma idioma)
    {
        try
        {
            if (idioma == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _uow.IdiomaRepository.Add(idioma);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterIdioma", new { id = idioma.IdiomaId }, idioma);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Idioma idioma)
    {
        try
        {
            if (id != idioma.IdiomaId)
            {
                return NotFound($"Idioma não encontrado. Id = {id}.");
            }

            _uow.IdiomaRepository.Update(idioma);
            _uow.Commit();

            return Ok(idioma);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public  ActionResult Delete(int id)
    {
        try
        {
            var idioma = _uow.IdiomaRepository.GetById(i => i.IdiomaId == id);

            if (idioma == null)
            {
                return BadRequest($"Idioma não encontrado. Id = {id}.");
            }

            _uow.IdiomaRepository.Delete(idioma);
            _uow.Commit();

            return Ok(idioma);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
