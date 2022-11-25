using SoloAdventureAPI.DTO;
using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.DTOs;

public class IdiomaDTO
{
    public int IdiomaId { get; set; }
    public string Nome { get; set; }
    public bool IdiomaAtivo { get; set; }
    public ICollection<Aventura> Aventuras { get; set; }
}
