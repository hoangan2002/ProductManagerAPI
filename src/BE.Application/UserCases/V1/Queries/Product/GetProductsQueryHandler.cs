﻿using System.Linq.Expressions;
using AutoMapper;
using BE.Contract.Abstractions.Message;
using BE.Contract.Abstractions.Shared;
using BE.Contract.Enumerations;
using BE.Contract.Services.Product;
using BE.Domain.Abstractions;
using BE.Domain.Abstractions.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BE.Application.UserCases.V1.Queries.Product;
public sealed class GetProductsQueryHandler : IQueryHandler<Query.GetProductsQuery, PagedResult<Response.ProductResponse>>
{
    private readonly IRepositoryBase<Domain.Entities.Product, Guid> _productRepository;
    private readonly IMapper _mapper;
    public GetProductsQueryHandler(IRepositoryBase<Domain.Entities.Product, Guid> productRepository,
    IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<Response.ProductResponse>>> Handle(Query.GetProductsQuery request, CancellationToken cancellationToken)
    {
        if (request.SortColumnAndOrder.Any()) // =>>  Raw Query when order by multi column
        {
            var PageIndex = request.PageIndex <= 0 ? PagedResult<Domain.Entities.Product>.DefaultPageIndex : request.PageIndex;
            var PageSize = request.PageSize <= 0
                ? PagedResult<Domain.Entities.Product>.DefaultPageSize
                : request.PageSize > PagedResult<Domain.Entities.Product>.UpperPageSize
                ? PagedResult<Domain.Entities.Product>.UpperPageSize : request.PageSize;

            // ============================================
            var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
                ? @$"SELECT * FROM {nameof(Domain.Entities.Product)} ORDER BY "
                : @$"SELECT * FROM {nameof(Domain.Entities.Product)}
                        WHERE {nameof(Domain.Entities.Product.Name)} LIKE '%{request.SearchTerm}%'
                        OR {nameof(Domain.Entities.Product.Description)} LIKE '%{request.SearchTerm}%'
                        ORDER BY ";

            foreach (var item in request.SortColumnAndOrder)
                productsQuery += item.Value == SortOrder.Descending
                    ? $"{item.Key} DESC, "
                    : $"{item.Key} ASC, ";

            productsQuery = productsQuery.Remove(productsQuery.Length - 2);

            productsQuery += $" OFFSET {(PageIndex - 1) * PageSize} ROWS FETCH NEXT {PageSize} ROWS ONLY";

            var products = await _productRepository.FromSqlRaw(productsQuery)
                .ToListAsync(cancellationToken: cancellationToken);

            var totalCount = await _productRepository.CountAsync();

            var productPagedResult = PagedResult<Domain.Entities.Product>.Create(products,
                PageIndex,
                PageSize,
                totalCount);

            var result = _mapper.Map<PagedResult<Response.ProductResponse>>(productPagedResult);

            return Result.Success(result);
        }
        else // =>> Entity Framework
        {
            var productsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? _productRepository.FindAll()
            : _productRepository.FindAll(x => x.Name.Contains(request.SearchTerm) || x.Description.Contains(request.SearchTerm));

            productsQuery = request.SortOrder == SortOrder.Descending
            ? productsQuery.OrderByDescending(GetSortProperty(request))
            : productsQuery.OrderBy(GetSortProperty(request));

            var products = await PagedResult<Domain.Entities.Product>.CreateAsync(productsQuery,
                request.PageIndex,
                request.PageSize);

            var result = _mapper.Map<PagedResult<Response.ProductResponse>>(products);
            return Result.Success(result);
        }
    }

    private static Expression<Func<Domain.Entities.Product, object>> GetSortProperty(Query.GetProductsQuery request)
         => request.SortColumn?.ToLower() switch
         {
             "name" => product => product.Name,
             "price" => product => product.Price,
             "description" => product => product.Description,
             _ => product => product.Id
             //_ => product => product.CreatedDate // Default Sort Descending on CreatedDate column
         };
}
