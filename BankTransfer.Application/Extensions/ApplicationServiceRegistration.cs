using BankTransfer.Application.Interfaces.Services;
using BankTransfer.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BankTransfer.Application.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITransferService, TransferService>();

        return services;
    }
}