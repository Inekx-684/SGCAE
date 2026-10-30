using SGCAE.Domain.Entities.AccessRecords;
using SGCAE.Domain.Entities.AccessPoints;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SGCAE.Domain.Entities.Employees;
using SGCAE.Domain.Entities.Credentials;

namespace SGCAE.Persistence.Context;

public sealed class DataContext : DbContext
{
    public DataContext(
        DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Credential> Credentials => Set<Credential>();

    public DbSet<AccessPoint> AccessPoints => Set<AccessPoint>();
    public DbSet<AccessRecord> AccessRecords => Set<AccessRecord>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(DataContext).Assembly);
    }
}
