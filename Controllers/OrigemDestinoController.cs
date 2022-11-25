using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.DTOs;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrigemDestinoController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public OrigemDestinoController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet("Destinos")]
    public ActionResult<IEnumerable<OrigemDestinoDTO>> GetDestinos ()
    {
        try
        {
            var origemDestino = _uow.OrigemDestinoRepository.GetDestinos().ToList();

            if (origemDestino == null)
            {
                return NotFound("Nenhum destino encontrado");
            }

            var origemDestinoDTO = _mapper.Map<List<OrigemDestinoDTO>>(origemDestino);

            return Ok(origemDestinoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet]
    public ActionResult<IEnumerable<OrigemDestinoDTO>> Get()
    {
        try
        {
            var origemDestino = _uow.OrigemDestinoRepository.Get().ToList();

            if (origemDestino is null)
            {
                return NotFound("Nenhum caminho encontrado.");
            }

            var origemDestinoDTO = _mapper.Map<List<OrigemDestinoDTO>>(origemDestino);

            return Ok(origemDestinoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpGet("{origemId:int}, {destinoId:int}", Name = "ObterOrigemDestino")]
    public ActionResult<OrigemDestinoDTO> Get(int origemId, int destinoId)
    {
        try
        {
            var origemDestino = _uow.OrigemDestinoRepository.GetById(od => od.PassoOrigemId == origemId && od.PassoDestinoId == destinoId);

            if (origemDestino == null)
            {
                return NotFound($"Caminho não encontrado. Origem Id = {origemId} | Destino Id = {destinoId}.");
            }

            var origemDestinoDTO = _mapper.Map<OrigemDestinoDTO>(origemDestino);

            return Ok(origemDestinoDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

    [HttpPost]
    public ActionResult Post(OrigemDestinoDTO origemDestinoDTO)
    {
        try
        {
            if (origemDestinoDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var origemDestino = _mapper.Map<OrigemDestino>(origemDestinoDTO);

            _uow.OrigemDestinoRepository.Add(origemDestino);
            _uow.Commit();

            var origemDestinoDTORetorno = _mapper.Map<OrigemDestinoDTO>(origemDestino);

            return new CreatedAtRouteResult("ObterOrigemDestino", new { origemId = origemDestino.PassoOrigemId, destinoId = origemDestino.PassoDestinoId }, origemDestinoDTORetorno);
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

            var origemDestinoDTO = _mapper.Map<OrigemDestinoDTO>(origemDestino);

            return Ok(origemDestinoDTO);

            //return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }
}
