using UniSyncApi.Repositories.Implementations;
using UniSyncApi.Repositories.Interfaces;
using UniSyncApi.Services.Implementations;
using UniSyncApi.Services.Interfaces;
using UniSyncApi.Utilities;

namespace UniSyncApi.Extensions;

public static class ServiceRegistry
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IStudentService, StudentService>();
        return services;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        return services;
    }

    public static IServiceCollection RegiserUtils(this IServiceCollection services)
    {
        services.AddSingleton<AuthUtil>();
        return services;
    }
}