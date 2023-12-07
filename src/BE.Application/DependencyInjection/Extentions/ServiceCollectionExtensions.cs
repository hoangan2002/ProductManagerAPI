using BE.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using BE.Application.Mapper;

namespace BE.Application.DependencyInjection.Extentions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
    => services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))
       // .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(BE.Contract.AssemblyReference.Assembly))
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        .AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);
    /*
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
      => services.AddMediatR(cfg =>
      cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))

      //.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>))
      .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
      .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
      .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))
      //.AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
      .AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true); 
    */
    public static IServiceCollection AddConfigureAutoMapper(this IServiceCollection services)
       => services.AddAutoMapper(typeof(ServiceProfile));
}
