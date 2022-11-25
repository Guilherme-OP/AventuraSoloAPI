using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.DTO;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AventurasController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public AventurasController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet("passos")]
    public async Task<ActionResult<IEnumerable<AventuraDTO>>> GetAventurasPassos()
    {
        try
        {
            var aventuras = await _uow.AventuraRepository.GetPassosPorAventura();

            if (aventuras is null)
            {
                return NotFound("Nenhuma aventura encontrada.");
            }

            var aventurasDTO = _mapper.Map<List<AventuraDTO>>(aventuras);

            return Ok(aventurasDTO);
        }
        catch (Exception)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AventuraDTO>>> Get()
    {
        try
        {
            var aventuras = await _uow.AventuraRepository.Get().ToListAsync();

            if (aventuras is null)
            {
                return NotFound("Nenhuma aventura encontrada.");
            }

            var aventurasDTO = _mapper.Map<List<AventuraDTO>>(aventuras);

            return Ok(aventurasDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{id:int}", Name = "ObterAventura")]
    public async Task<ActionResult<AventuraDTO>> Get(int id)
    {
        try
        {
            var aventura = await _uow.AventuraRepository.GetById(a => a.AventuraId == id);

            if (aventura == null)
            {
                return NotFound($"Aventura não encontrada. Id = {id}.");
            }

            var aventuraDTO = _mapper.Map<AventuraDTO>(aventura);

            return Ok(aventuraDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }


    }

    [HttpPost]
    public async Task<ActionResult> Post(AventuraDTO aventuraDTO)
    {
        try
        {
            if (aventuraDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var aventura = _mapper.Map<Aventura>(aventuraDTO);

            _uow.AventuraRepository.Add(aventura);
            await _uow.Commit();

            var aventuraDTORetorno = _mapper.Map<AventuraDTO>(aventura);

            return new CreatedAtRouteResult("ObterAventura", new { id = aventura.AventuraId }, aventuraDTORetorno);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, AventuraDTO aventuraDTO)
    {
        try
        {
            if (id != aventuraDTO.AventuraId)
            {
                return NotFound($"Dados inválidos.  Id = {id}.");
            }

            var aventura = _mapper.Map<Aventura>(aventuraDTO);

            _uow.AventuraRepository.Update(aventura);
            await _uow.Commit();

            var aventuraDTORetorno = _mapper.Map<AventuraDTO>(aventura);

            return Ok(aventuraDTORetorno);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<AventuraDTO>> Delete(int id)
    {
        try
        {
            var aventura = await _uow.AventuraRepository.GetById(a => a.AventuraId == id);

            if (aventura == null)
            {
                return BadRequest($"Aventura não encontrada. Id = {id}.");
            }

            _uow.AventuraRepository.Delete(aventura);
            await _uow.Commit();

            var aventuraDTO = _mapper.Map<AventuraDTO>(aventura);

            return Ok(aventuraDTO);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
