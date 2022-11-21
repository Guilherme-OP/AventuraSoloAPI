using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SoloAdventureAPI.Models;

public class Idioma
{
    public int IdiomaId { get; set; }

    [StringLength(50)]
    public string? Nome { get; set; }
    public bool IdiomaAtivo { get; set; }

    // Relacionamento Aventura 1 x 1 Idioma

    public ICollection<Aventura>? Aventuras { get; set; }
}
