using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Domain.Abstractions;

namespace BE.Persistance;
public class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EFUnitOfWork(ApplicationDbContext context)
        => _context = context;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync();

    async ValueTask IAsyncDisposable.DisposeAsync()
        => await _context.DisposeAsync();
}
