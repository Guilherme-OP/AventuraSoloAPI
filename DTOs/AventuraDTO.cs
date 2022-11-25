using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.DTO;

public class AventuraDTO
{
    public int AventuraId { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool AventuraAtiva {  get; set; }
    public DateTime DataCadastro { get; set; }
    public ICollection<Passo>? Passos { get; set; }
}
