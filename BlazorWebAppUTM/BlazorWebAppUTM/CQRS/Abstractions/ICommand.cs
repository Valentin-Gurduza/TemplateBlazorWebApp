using MediatR;

namespace BlazorWebAppUTM.CQRS.Abstractions;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}