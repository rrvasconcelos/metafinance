using System.Reflection;
using FluentValidation;
using Mapster;
using MediatR;
using MetaFinance.Application.Common.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace MetaFinance.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        
        TypeAdapterConfig.GlobalSettings.Default
            .MapToConstructor(true);

        return services;
    }
}