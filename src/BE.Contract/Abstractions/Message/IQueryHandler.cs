using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Contract.Abstractions.Shared;
using MediatR;

namespace BE.Contract.Abstractions.Message;
public interface  IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery,Result<TResponse>> where TQuery : IQuery<TResponse>
{
}
