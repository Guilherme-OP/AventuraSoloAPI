using SoloAdventureAPI.Models;

namespace SoloAdventureAPI.Repository;

public interface IIdiomaRepository : IRepository<Idioma>
{
    Task<IEnumerable<Idioma>> GetAventurasPorIdioma();
}
