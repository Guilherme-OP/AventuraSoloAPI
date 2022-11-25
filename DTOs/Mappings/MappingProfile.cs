using AutoMapper;
using SoloAdventureAPI.DTO;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Aventura, AventuraDTO>().ReverseMap();
        CreateMap<Passo, PassoDTO>().ReverseMap();
        CreateMap<OrigemDestino, OrigemDestinoDTO>().ReverseMap();
    }
}
