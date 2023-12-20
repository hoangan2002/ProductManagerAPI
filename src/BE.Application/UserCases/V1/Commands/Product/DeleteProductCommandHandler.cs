using BE.Contract.Abstractions.Message;
using BE.Contract.Abstractions.Shared;
using BE.Domain.Abstractions.Repositories;
using BE.Domain.Abstractions;
using BE.Domain.Exceptions;
using MediatR;
using BE.Contract.Services.Product;

namespace BE.Application.UserCases.V1.Commands.Product;
public sealed class DeleteProductCommandHandler : ICommandHandler<Command.DeleteProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    private readonly IPublisher _publisher;

    public DeleteProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.DeleteProductCommand request, CancellationToken cancellationToken)
    {
        
        var product = await _productRepository.FindByIdAsync(request.Id) ?? throw new ProductException.ProductNotFoundException(request.Id); 
        _productRepository.Remove(product);
        _unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}
