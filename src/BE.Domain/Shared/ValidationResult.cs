using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Shared;
public sealed class ValidationResult : Result, IValidationResult
{
    public Error[] Errors { get; }
    private ValidationResult(Error[] errors)
        : base(false, IValidationResult.ValidationError) =>
        Errors = errors;
    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}
