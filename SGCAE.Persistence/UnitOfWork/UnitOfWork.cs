using System;
using System.Collections.Generic;
using System.Text;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Persistence.Context;

namespace SGCAE.Persistence.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(
            cancellationToken);
    }
}
