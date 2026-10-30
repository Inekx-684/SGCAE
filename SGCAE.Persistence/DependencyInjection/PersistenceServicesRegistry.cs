using SGCAE.Persistence.Repositories.AccessRecords;
using SGCAE.Persistence.Repositories.AccessPoints;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGCAE.Application.Interfaces.Persistence;
using SGCAE.Application.Interfaces.Repositories;
using SGCAE.Persistence.Context;
using SGCAE.Persistence.Repositories.Employees;
using SGCAE.Persistence.Repositories.Credentials;

namespace SGCAE.Persistence.DependencyInjection;

public static class PersistenceServicesRegistry
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "La cadena de conexión DefaultConnection no está configurada.");
        }

        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<ICredentialsRepository, CredentialsRepository>();

        services.AddScoped<IAccessPointsRepository, AccessPointsRepository>();
        services.AddScoped<IAccessRecordsRepository, AccessRecordsRepository>();

        services.AddScoped<IUnitOfWork,
            SGCAE.Persistence.UnitOfWork.UnitOfWork>();

        return services;
    }
}
