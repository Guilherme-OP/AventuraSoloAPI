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
    public string Descricao { get; set; } = null!;

    public bool? AventuraAtiva { get; set; }

    public DateTime DataCadastro { get; set; } = DateTime.Now;


    // ------------- Propriedades de Navegação -------------

    // Relacionamento Aventura 1 x N Passos
    public ICollection<Passo>? Passos { get; set; }
}