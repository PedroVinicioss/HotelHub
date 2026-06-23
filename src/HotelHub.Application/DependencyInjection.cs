using HotelHub.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace HotelHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator.Mediator>();

        // Scrutor: varre o assembly e registra todos os IRequestHandler<,> automaticamente
        services.Scan(scan => scan
            .FromAssemblyOf<Mediator.Mediator>()
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}