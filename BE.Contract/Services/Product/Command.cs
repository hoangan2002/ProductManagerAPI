using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Contract.Abstractions.Message;

namespace BE.Contract.Services.Product;
public static class Command 
{
    public record CreateProduct(string Name, decimal Price,string Description) : ICommand;
    public record UpdateProduct(Guid Id, string Name, decimal Price, string Description) : ICommand;
    public record DeleteProduct(Guid Id) : ICommand;
}
