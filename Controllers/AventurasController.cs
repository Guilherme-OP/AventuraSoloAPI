using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AventurasController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public AventurasController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet("passos")]
    public ActionResult<IEnumerable<Aventura>> GetAventurasPassos()
    {
        try
        {
            var aventuras = _uow.AventuraRepository.GetPassosPorAventura().ToList();

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
    public ActionResult<IEnumerable<Aventura>> Get()
    {
        try
        {
            var aventuras = _uow.AventuraRepository.Get().ToList();
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
    public ActionResult<Aventura> Get(int id)
    {
        try
        {
            var aventura = _uow.AventuraRepository.GetById(a => a.AventuraId == id);

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
    public ActionResult Post(Aventura aventura)
    {
        try
        {
            if (aventura == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _uow.AventuraRepository.Add(aventura);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterAventura", new { id = aventura.AventuraId }, aventura);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut]
    public ActionResult Put(int id, Aventura aventura)
    {
        try
        {
            if (id != aventura.AventuraId)
            {
                return NotFound($"Dados inválidos.  Id = {id}.");
            }

            _uow.AventuraRepository.Update(aventura);
            _uow.Commit();

            return Ok(aventura);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        } 
    }

    [HttpDelete]
    public ActionResult Delete(int id)
    {
        try
        {
            var aventura = _uow.AventuraRepository.GetById(a => a.AventuraId == id);

            if (aventura == null)
            {
                return BadRequest($"Aventura não encontrada. Id = {id}.");
            }

            _uow.AventuraRepository.Delete(aventura);
            _uow.Commit();

            return Ok(aventura);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
