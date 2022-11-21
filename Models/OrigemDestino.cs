using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SoloAdventureAPI.Models
{
    public class OrigemDestino
    {
        public int PassoOrigemId { get; set; }
        public int PassoDestinoId { get; set; }

        // ------------- Propriedades de Navegação -------------

        // Relacionamento Passos N x N Passos
        [JsonIgnore]
        public virtual Passo? PassoOrigem { get; set; }
        [JsonIgnore]
        public virtual Passo? PassoDestino { get; set; }
    }
}
