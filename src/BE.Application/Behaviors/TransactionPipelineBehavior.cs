using System.Transactions;
using BE.Domain.Abstractions;
using MediatR;

namespace BE.Application.Behaviors;
public sealed class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    public TransactionPipelineBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
     public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) // In case TRequest is QueryRequest just ignore
            return await next();

        #region ============== SQL-SERVER-STRATEGY-1 ==============

        //// use of an ef core resiliency strategy when using multiple dbcontexts within an explicit begintransaction():
        //// https://learn.microsoft.com/ef/core/miscellaneous/connection-resiliency
        //var strategy = _context.database.createexecutionstrategy();
        //return await strategy.executeasync(async () =>
        //{
        //    await using var transaction = await _context.database.begintransactionasync();
        //    {
        //        var response = await next();
        //        await _context.savechangesasync();
        //        await transaction.commitasync();
        //        return response;
        //    }
        //});
        #endregion ============== SQL-SERVER-STRATEGY-1 ==============

        #region ============== SQL-SERVER-STRATEGY-2 ==============

        using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();
            await _unitOfWork.SaveChangesAsync();
            transaction.Complete();
            await _unitOfWork.DisposeAsync();
            return response;
        }
        #endregion ============== SQL-SERVER-STRATEGY-2 ==============

    }

    private bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");
}
