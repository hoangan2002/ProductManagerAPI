using BE.Contract.Abstractions.Message;
using BE.Contract.Abstractions.Shared;
using BE.Domain.Abstractions.Repositories;
using BE.Domain.Abstractions;
using MediatR;
using BE.Contract.Services.Product;

namespace BE.Application.UserCases.V1.Commands.Order;
public sealed class CreateProductCommandHandler : ICommandHandler<Command.CreateProductCommand>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    private readonly IPublisher _publisher;

    public CreateProductCommandHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
        IUnitOfWork unitOfWork,
        IPublisher publisher
        )
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _publisher = publisher;
    }

    public async Task<Result> Handle(Command.CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.Product.CreateProduct(Guid.NewGuid(), request.Name, request.Price, request.Description);

        _productRepository.Add(product);

       await  _unitOfWork.SaveChangesAsync();
        await _publisher.Publish(new DomainEvent.ProductCreated(Guid.NewGuid()));
        return Result.Success();
    }
}
