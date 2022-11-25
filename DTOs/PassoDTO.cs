using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.DTOs;

public class PassoDTO
{
    public int PassoId { get; set; }
    public string Nome { get; set; }
    public string Texto { get; set; }
    public string ImageUrl { get; set; }
    public bool PrimeiroPasso { get; set; }
    public int AventuraId {  get; set; }
    public virtual ICollection<OrigemDestino>? Origens { get; set; }
}
