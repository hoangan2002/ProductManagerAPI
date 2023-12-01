using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BE.Contract.Services.Product.Validators;
public class CreateProductValidator : AbstractValidator<Command.CreateProduct>
{
    public CreateProductValidator() {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
    }
}
