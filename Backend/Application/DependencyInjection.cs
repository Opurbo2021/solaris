using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Interfaces.Services;
using Application.Services;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        //Services
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped<IEnergyProductionService, EnergyProductionService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IInstallationService, InstallationService>();
        services.AddScoped<IInstallationStatusHistoryService, InstallationStatusHistoryService>();
        services.AddScoped<IInstallationTechnicianService, InstallationTechnicianService>();
        services.AddScoped<ISupportTicketService, SupportTicketService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWeatherDataService, WeatherDataService>();
        
        return services;
    }
}