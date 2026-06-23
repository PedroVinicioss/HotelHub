using HotelHub.Domain.Abstraction;

namespace HotelHub.Application.Abstractions;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
