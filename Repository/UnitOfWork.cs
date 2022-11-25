using SoloAdventureAPI.Context;

namespace SoloAdventureAPI.Repository;

public class UnitOfWork : IUnitOfWork
{
    private AventuraRepository _aventuraRepository;
    private PassoRepository _passoRepository;
    private OrigemDestinoRepository _origemDestinoRepository;
    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IAventuraRepository AventuraRepository
    {
        get { return _aventuraRepository = _aventuraRepository ?? new AventuraRepository(_context); }
    }

    public IPassoRepository PassoRepository
    {
        get { return _passoRepository = _passoRepository ?? new PassoRepository(_context); }
    }

    public IOrigemDestinoRepository OrigemDestinoRepository
    {
        get { return _origemDestinoRepository = _origemDestinoRepository ?? new OrigemDestinoRepository(_context); }
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
