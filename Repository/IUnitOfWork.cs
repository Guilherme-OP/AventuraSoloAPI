namespace SoloAdventureAPI.Repository;

public interface IUnitOfWork
{
    IIdiomaRepository IdiomaRepository { get; }
    IAventuraRepository AventuraRepository { get; }
    IPassoRepository PassoRepository { get; }
    IOrigemDestinoRepository OrigemDestinoRepository { get; }

    Task Commit();
}
