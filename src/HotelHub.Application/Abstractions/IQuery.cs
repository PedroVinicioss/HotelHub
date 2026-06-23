using HotelHub.Domain.Abstraction;

namespace HotelHub.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
