﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoloAdventureAPI.Context;
using SoloAdventureAPI.DTOs;
using SoloAdventureAPI.Models;
using SoloAdventureAPI.Repository;

namespace SoloAdventureAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PassosController : ControllerBase
{
	private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

	public PassosController(IUnitOfWork uow, IMapper mapper)
	{
        _uow = uow;
        _mapper = mapper;
	}

    [HttpGet("destinos")]
    public ActionResult<IEnumerable<PassoDTO>> GetPassosOrigensDestinos()
    {
        try
        {
            var passos = _uow.PassoRepository.GetPassosOrigemDestinos().ToList();

            if (passos is null)
            {
                return NotFound("Nenhum passo encontrado.");
            }

            var passosDTO = _mapper.Map<List<PassoDTO>>(passos);

            return Ok(passosDTO);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
    }

	[HttpGet]
	public ActionResult<IEnumerable<PassoDTO>> Get()
	{
		try
		{
            var passos = _uow.PassoRepository.Get().ToList();

            if (passos is null)
            {
                return NotFound("Nenhum passo encontrado.");
            }

            var passosDTO = _mapper.Map<List<PassoDTO>>(passos);

            return Ok(passos);
        }
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpGet("{id:int}", Name = "ObterPasso")]
	public ActionResult<PassoDTO> Get(int id) 
	{
		try
		{
            var passo = _uow.PassoRepository.GetById(p => p.PassoId == id);

            if (passo == null)
            {
                return NotFound($"Passo não encontrado. Id = {id}.");
            }

            var passoDTO = _mapper.Map<PassoDTO>(passo);

            return Ok(passoDTO);
        }
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpPost]
	public ActionResult Post(PassoDTO passoDTO)
	{
		try
		{
            if (passoDTO == null)
            {
                return BadRequest("Dados inválidos.");
            }

            var passo = _mapper.Map<Passo>(passoDTO);

            _uow.PassoRepository.Add(passo);
            _uow.Commit();

            var passoDTORetorno = _mapper.Map<PassoDTO>(passo);

            return new CreatedAtRouteResult("ObterPasso", new { id = passo.PassoId }, passoDTORetorno);
        }
		catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpPut("{id:int}")]
	public ActionResult Put(int id, PassoDTO passoDTO) 
	{
		try
		{
            if (id != passoDTO.PassoId)
            {
                return BadRequest($"Dados inválidos. Id = {id}.");
            }

            var passo = _mapper.Map<Passo>(passoDTO);

            _uow.PassoRepository.Update(passo);
            _uow.Commit();

            var passoDTORetorno = _mapper.Map<PassoDTO>(passo);

            return Ok(passoDTORetorno);

            //return NoContent();
        }
        catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}

	[HttpDelete("{id:int}")]
    public ActionResult<PassoDTO> Delete(int id)
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

            var passoDTO = _mapper.Map<PassoDTO>(passo);

            return Ok(passoDTO);

            //return NoContent();
        }
        catch (Exception)
		{
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
        }
	}
}
