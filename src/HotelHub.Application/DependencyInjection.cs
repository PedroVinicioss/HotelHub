using FluentValidation;
using HotelHub.Application.Abstractions;
using HotelHub.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace HotelHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMediator, Mediator.Mediator>();

        services.Scan(scan => scan
            .FromAssemblyOf<Mediator.Mediator>()
            .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Decora todos os handlers com o ValidationBehavior
        services.Decorate(typeof(IRequestHandler<,>), typeof(ValidationBehavior<,>));

        // Registra todos os validators do assembly automaticamente
        services.AddValidatorsFromAssemblyContaining<Mediator.Mediator>(
            lifetime: ServiceLifetime.Scoped,
            includeInternalTypes: true);

        return services;
    }
}
