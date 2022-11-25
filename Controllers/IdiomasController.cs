using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SoloAdventureAPI.DTOs;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class IdiomasController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public IdiomasController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet("IdiomaAventuras")]
    public ActionResult<IEnumerable<IdiomaDTO>> GetIdiomasAventuras()
    {
        try
        {
            var idiomas = _uow.IdiomaRepository.GetAventurasPorIdioma().ToList();

            if (idiomas is null)
            {
                return NotFound("Nenhum idioma encontrado");
            }

            var idiomasDTO = _mapper.Map<List<IdiomaDTO>>(idiomas);

            return Ok(idiomasDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<IdiomaDTO>> Get()
    {
        try
        {
            var idiomas = _uow.IdiomaRepository.Get().ToList();

            if (idiomas is null)
            {
                return NotFound("Idioma não encontrado.");
            }

            var idiomasGetDTO = _mapper.Map<List<IdiomaDTO>>(idiomas);

            return Ok(idiomasGetDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:int}", Name = "ObterIdioma")]
    public ActionResult<IdiomaDTO> Get(int id)
    {
        try
        {
            var idioma = _uow.IdiomaRepository.GetById(i => i.IdiomaId == id);

            if (idioma == null)
            {
                return NotFound($"Idioma não encontrado. Id = {id}.");
            }

            var idiomaDTO = _mapper.Map<IdiomaDTO>(idioma);

            return Ok(idiomaDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public ActionResult Post(IdiomaDTO idiomaDTO)
    {
        try
        {
            if (idiomaDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var idioma = _mapper.Map<Idioma>(idiomaDTO);

            _uow.IdiomaRepository.Add(idioma);
            _uow.Commit();

            var idiomaDTORetorno = _mapper.Map<IdiomaDTO>(idioma);

            return new CreatedAtRouteResult("ObterIdioma", new { id = idioma.IdiomaId }, idiomaDTORetorno);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, IdiomaDTO idiomaDTO)
    {
        try
        {
            if (id != idiomaDTO.IdiomaId)
            {
                return NotFound($"Idioma não encontrado. Id = {id}.");
            }

            var idioma = _mapper.Map<Idioma>(idiomaDTO);

            _uow.IdiomaRepository.Update(idioma);
            _uow.Commit();

            var idiomaDTORetorno = _mapper.Map<IdiomaDTO>(idioma);

            return Ok(idiomaDTORetorno);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
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

            var idiomaDTO = _mapper.Map<IdiomaDTO>(idioma);

            return Ok(idiomaDTO);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
