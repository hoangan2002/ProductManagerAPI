using BE.Contract.Abstractions.Shared;
using MediatR;

namespace BE.Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
