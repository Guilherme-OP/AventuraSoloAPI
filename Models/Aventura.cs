using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SoloAdventureAPI.Models;

public class Aventura
{
    public int AventuraId { get; set; }

    [StringLength(100)]
    public string Titulo { get; set; } = null!;

    [StringLength(300)]
    public string DescricaoRapida { get; set; } = null!;

    public bool? AventuraAtiva { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;

    public DateTime DataAtualizada { get; set; } = new DateTime();

    public float Versao { get; set; }

    [StringLength(300)]
    public string? ImagemUrl { get; set; }

    // ------------- Propriedades de Navegação -------------
    public int IdiomaId { get; set; }

    // Relacionamento Aventura 1 x N Passos
    public ICollection<Passo>? Passos { get; set; }

    // Relacionamento Aventura 1 x 1 Idioma
    [JsonIgnore]
    public Idioma? Idioma { get; set; }
}