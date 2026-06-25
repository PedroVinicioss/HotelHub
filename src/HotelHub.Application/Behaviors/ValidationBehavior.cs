using FluentValidation;
using HotelHub.Application.Abstractions;
using HotelHub.Domain.Abstraction;

namespace HotelHub.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(
    IRequestHandler<TRequest, TResponse> inner,
    IEnumerable<IValidator<TRequest>> validators)
    : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        if (!validators.Any())
            return await inner.HandleAsync(request, cancellationToken);

        var context = new ValidationContext<TRequest>(request);

        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0)
            return await inner.HandleAsync(request, cancellationToken);

        var error = Error.Validation(
            failures[0].PropertyName,
            failures[0].ErrorMessage);

        return CreateFailure(error);
    }

    private static TResponse CreateFailure(Error error)
    {
        if (typeof(TResponse) == typeof(Result))
            return (TResponse)(object)Result.Failure(error);

        var valueType = typeof(TResponse).GetGenericArguments()[0];
        var method = typeof(Result)
            .GetMethods()
            .First(m => m.Name == nameof(Result.Failure) && m.IsGenericMethod)
            .MakeGenericMethod(valueType);

        return (TResponse)method.Invoke(null, [error])!;
    }
}
