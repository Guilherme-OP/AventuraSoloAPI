using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.DTO;

public class AventuraDTO
{
    public int AventuraId { get; set; }
    public string Titulo { get; set; }
    public string DescricaoRapida { get; set; }
    public bool AventuraAtiva {  get; set; }
    public DateTime DataCadastro { get; set; }
    public string ImagemUrl { get; set; }
    public int IdiomaId { get; set; }
    public ICollection<Passo> Passos { get; set; }
}
