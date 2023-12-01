using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Domain.Shared;
using MediatR;

namespace BE.Application.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
