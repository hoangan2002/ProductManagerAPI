using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Contract.Abstractions.Shared;
using MediatR;

namespace BE.Contract.Abstractions.Message;
public interface ICommand : IRequest<Result>
{
}
public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}
