using MediatR;

namespace BlazorWebAppUTM.CQRS.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}