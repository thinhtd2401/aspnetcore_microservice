using Contracts.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
{
    private readonly TContext _context;
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public UnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<int> CommitAsync()
    {
        return _context.SaveChangesAsync();
    }
}