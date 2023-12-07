using BE.Contract.Abstractions.Message;
using BE.Contract.Abstractions.Shared;
using BE.Contract.Enumerations;
using static BE.Contract.Services.Product.Response;

namespace BE.Contract.Services.Product;
public static class Query
{
    public record GetProductsQuery(string? SearchTerm, string? SortColumn, SortOrder? SortOrder, IDictionary<string, SortOrder>? SortColumnAndOrder, int PageIndex, int PageSize) : IQuery<PagedResult<ProductResponse>>;
    public record GetProductByIdQuery(Guid Id) : IQuery<ProductResponse>;
}
