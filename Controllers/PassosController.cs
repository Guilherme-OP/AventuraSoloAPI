using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PassosController : ControllerBase
{
	private readonly IUnitOfWork _uow;

	public PassosController(IUnitOfWork uow)
	{
        _uow = uow;
	}

    [HttpGet("destinos")]
    public ActionResult<IEnumerable<Passo>> GetPassosOrigensDestinos()
    {
        try
        {
            var passos = _uow.PassoRepository.GetPassosOrigemDestinos().ToList();

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
	public ActionResult<IEnumerable<Passo>> Get()
	{
		try
		{
            var passos = _uow.PassoRepository.Get().ToList();
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
	public ActionResult<Passo> Get(int id) 
	{
		try
		{
            var passo = _uow.PassoRepository.GetById(p => p.PassoId == id);

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
	public ActionResult Post(Passo passo)
	{
		try
		{
            if (passo == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _uow.PassoRepository.Add(passo);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterPasso", new { id = passo.PassoId }, passo);
        }
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpPut]
	public ActionResult Put(int id, Passo passo) 
	{
		try
		{
            if (id != passo.PassoId)
            {
                return BadRequest($"Dados inválidos. Id = {id}.");
            }

            _uow.PassoRepository.Update(passo);
            _uow.Commit();

            return Ok(passo);

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
            var passo = _uow.PassoRepository.GetById(p => p.PassoId == id);

            if (passo == null)
            {
                return BadRequest($"Passo não encontrada. Id = {id}.");
            }

            _uow.PassoRepository.Delete(passo);
            _uow.Commit();

            return Ok(passo);

            //return NoContent();
        }
        catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}
}
