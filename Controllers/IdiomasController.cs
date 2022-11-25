using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<ActionResult<IEnumerable<IdiomaDTO>>> GetIdiomasAventuras()
    {
        try
        {
            var idiomas = await _uow.IdiomaRepository.GetAventurasPorIdioma();

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
    public async Task<ActionResult<IEnumerable<IdiomaDTO>>> Get()
    {
        try
        {
            var idiomas = await _uow.IdiomaRepository.Get().ToListAsync();

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
    public async Task<ActionResult<IdiomaDTO>> Get(int id)
    {
        try
        {
            var idioma = await _uow.IdiomaRepository.GetById(i => i.IdiomaId == id);

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
    public async Task<ActionResult> Post(IdiomaDTO idiomaDTO)
    {
        try
        {
            if (idiomaDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var idioma = _mapper.Map<Idioma>(idiomaDTO);

            _uow.IdiomaRepository.Add(idioma);
            await _uow.Commit();

            var idiomaDTORetorno = _mapper.Map<IdiomaDTO>(idioma);

            return new CreatedAtRouteResult("ObterIdioma", new { id = idioma.IdiomaId }, idiomaDTORetorno);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, IdiomaDTO idiomaDTO)
    {
        try
        {
            if (id != idiomaDTO.IdiomaId)
            {
                return NotFound($"Idioma não encontrado. Id = {id}.");
            }

            var idioma = _mapper.Map<Idioma>(idiomaDTO);

            _uow.IdiomaRepository.Update(idioma);
            await _uow.Commit();

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
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var idioma = await _uow.IdiomaRepository.GetById(i => i.IdiomaId == id);

            if (idioma == null)
            {
                return BadRequest($"Idioma não encontrado. Id = {id}.");
            }

            _uow.IdiomaRepository.Delete(idioma);
            await _uow.Commit();

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
