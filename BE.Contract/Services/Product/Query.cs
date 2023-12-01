using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Contract.Abstractions.Message;
using static BE.Contract.Services.Product.Response;

namespace BE.Contract.Services.Product;
public static class Query
{
    public record GetProductQuery() : IQuery<List<ProductResponse>>;
    public record GetProductById(Guid Id): IQuery<ProductResponse>;

}
