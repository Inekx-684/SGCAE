using SGCAE.Application.Features.AccessRecords.Commands.RegisterAccess;
using SGCAE.Application.Features.AccessRecords.Queries.GetAccessHistory;
using SGCAE.Application.Features.AccessPoints.Commands.CreateAccessPoint;
using SGCAE.Application.Features.AccessPoints.Commands.ActivateAccessPoint;
using SGCAE.Application.Features.AccessPoints.Commands.DeactivateAccessPoint;
using SGCAE.Application.Features.AccessPoints.Queries.GetAccessPointById;
using SGCAE.Application.Features.AccessPoints.Queries.GetAccessPointsList;
using SGCAE.Application.Features.Credentials.Commands.AssignCredential;
using SGCAE.Application.Features.Credentials.Commands.BlockCredential;
using SGCAE.Application.Features.Credentials.Commands.RevokeCredential;
using SGCAE.Application.Features.Credentials.Queries.GetCredentialById;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SGCAE.Application.Features.Employees.Commands.CreateEmployee;
using SGCAE.Application.Features.Employees.Commands.UpdateEmployee;
using SGCAE.Application.Features.Employees.Commands.DeactivateEmployee;
using SGCAE.Application.Features.Employees.Queries.GetEmployeeById;
using SGCAE.Application.Features.Employees.Queries.GetEmployeesList;
namespace SGCAE.Application.DependencyInjection;
public static class ApplicationServicesRegistry
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();
        services.AddScoped<RegisterAccessUseCase>();
        services.AddScoped<GetAccessHistoryUseCase>();
        services.AddScoped<CreateEmployeeUseCase>();
        services.AddScoped<GetEmployeeByIdUseCase>();
        services.AddScoped<GetEmployeesListUseCase>();
        services.AddScoped<UpdateEmployeeUseCase>();
        services.AddScoped<DeactivateEmployeeUseCase>();
        services.AddScoped<AssignCredentialUseCase>();
        services.AddScoped<GetCredentialByIdUseCase>();
        services.AddScoped<BlockCredentialUseCase>();
        services.AddScoped<RevokeCredentialUseCase>();
        services.AddScoped<CreateAccessPointUseCase>();
        services.AddScoped<GetAccessPointByIdUseCase>();
        services.AddScoped<GetAccessPointsListUseCase>();
        services.AddScoped<ActivateAccessPointUseCase>();
        services.AddScoped<DeactivateAccessPointUseCase>();
        return services;
    }
}
