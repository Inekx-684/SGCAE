using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SGCAE.Application.Features.Employees.Commands.CreateEmployee;
using SGCAE.Application.Features.Employees.Queries.GetEmployeeById;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGCAE.Application.DependencyInjection;

public static class ApplicationServicesRegistry
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // Registrar automáticamente los validadores
        services.AddValidatorsFromAssemblyContaining<
            CreateEmployeeCommandValidator>();

        // Registrar el caso de uso de empleados
        services.AddScoped<CreateEmployeeUseCase>();

        // Query: consultar empleado por ID
        services.AddScoped<GetEmployeeByIdUseCase>();


        return services;
    }
}
