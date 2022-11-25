using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SoloAdventureAPI.Models;

public class Passo
{
    public int PassoId { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(3000)]
    public string Texto { get; set; } = null!;

    public bool Inicio { get; set; }

    public bool PassoAtivo { get; set; }

    // ------------- Propriedades de Navegação -------------

    public int AventuraId { get; set; }

    // Relacionamento Aventura 1 x N Passos
    [JsonIgnore]
    public Aventura? Aventura { get; set; }

    // Relacionamento Passos N x N Passos
    //[JsonIgnore]
    public virtual ICollection<OrigemDestino>? Origens { get; set; }

    //[JsonIgnore]
    public virtual ICollection<OrigemDestino>? Destinos { get; set; }
}