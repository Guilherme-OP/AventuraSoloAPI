using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrigemDestinoController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public OrigemDestinoController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    [HttpGet("Destinos")]
    public ActionResult<IEnumerable<OrigemDestino>> GetDestinos ()
    {
        try
        {
            var origemDestino = _uow.OrigemDestinoRepository.GetDestinos().ToList();

            if (origemDestino == null)
            {
                return NotFound("Nenhum destino encontrado");
            }
            return Ok(origemDestino);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrigemDestino>> Get()
    {
        try
        {
            var origemDestino = _uow.OrigemDestinoRepository.Get().ToList();
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
    public ActionResult<OrigemDestino> Get(int origemId, int destinoId)
    {
        try
        {
            var origemDestino = _uow.OrigemDestinoRepository.GetById(od => od.PassoOrigemId == origemId && od.PassoDestinoId == destinoId);
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
    public ActionResult Post(OrigemDestino origemDestino)
    {
        try
        {
            if (origemDestino == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _uow.OrigemDestinoRepository.Add(origemDestino);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterOrigemDestino", new { origemId = origemDestino.PassoOrigemId, destinoId = origemDestino.PassoDestinoId }, origemDestino);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete]
    public ActionResult Delete(int origemId, int destinoId)
    {
        try
        {
            var origemDestino = _uow.OrigemDestinoRepository.GetById(od => od.PassoOrigemId == origemId && od.PassoDestinoId == destinoId);

            if (origemDestino == null)
            {
                return BadRequest($"Caminho não encontrado. Origem Id = {origemId} | Destino Id = {destinoId}.");
            }

            _uow.OrigemDestinoRepository.Delete(origemDestino);
            _uow.Commit();
            return Ok(origemDestino);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
